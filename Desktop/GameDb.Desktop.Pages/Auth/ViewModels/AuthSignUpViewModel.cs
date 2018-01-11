using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GameDb.Base.Interfaces;
using GameDb.DAL.Entities;
using GameDb.Desktop.Base;
using GameDb.Desktop.Base.BaseTypes;
using GameDb.Desktop.Base.Interfaces;
using Microsoft.Practices.ServiceLocation;
using Prism.Commands;
using Prism.Logging;
using Prism.Regions;

namespace GameDb.Desktop.Pages.Auth.ViewModels
{
    public class AuthSignUpViewModel : NavigationViewModelBase
    {
        public AuthSignUpViewModel()
        {
            _userEntityData = new UserEntity();
            SignUpCommand = new DelegateCommand<List<object>>(SignUp);
        }

        private readonly UserEntity _userEntityData;

        public string Login
        {
            get => _userEntityData.Login;
            set => SetProperty(ref _userEntityData.Login, value);
        }

        public string FirstName
        {
            get => _userEntityData.FirstName;
            set => SetProperty(ref _userEntityData.FirstName, value);
        }

        public string LastName
        {
            get => _userEntityData.LastName;
            set => SetProperty(ref _userEntityData.LastName, value);
        }

        public string Email
        {
            get => _userEntityData.Email;
            set => SetProperty(ref _userEntityData.Email, value);
        }

        public ICommand SignUpCommand { get; }

        private void SignUp(List<object> param)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Login))
                {
                    ServiceLocator.Current.GetInstance<ISimpleMessageBoxService>().
                                   Show("Enter login", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!(param[0] is PasswordBox pwBox) || !(param[1] is PasswordBox confirmPwBox))
                {
                    return;
                }

                var password = pwBox.Password;
                var confirmPassword = confirmPwBox.Password;

                if (password != confirmPassword)
                {
                    ServiceLocator.Current.GetInstance<ISimpleMessageBoxService>().
                                   Show("Password does not match the confirm password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var registrationService = ServiceLocator.Current.GetInstance<IRegistrationService>();
                registrationService.RegisterNewUser(_userEntityData, password);

                Navigator.ClearNavigationJournal();
                Navigator.NavigateTo(PageNames.AuthPage);
            }
            catch (Exception e)
            {
                var logger = ServiceLocator.Current.GetInstance<ILoggerFacade>();
                logger.Log($"An unexpected error in AuthPageSignInViewModel: {e.Message}", Category.Exception, Priority.Medium);
                ServiceLocator.Current.GetInstance<ISimpleMessageBoxService>().
                               Show($"An unexpected error.\n{e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region INavigationAware

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        #endregion
    }
}
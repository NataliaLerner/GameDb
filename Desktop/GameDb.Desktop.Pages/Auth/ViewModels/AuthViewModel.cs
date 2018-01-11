using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GameDb.Base.Interfaces;
using GameDb.Desktop.Base;
using GameDb.Desktop.Base.BaseTypes;
using GameDb.Desktop.Base.Interfaces;
using Microsoft.Practices.ServiceLocation;
using Prism.Commands;
using Prism.Logging;
using Prism.Regions;

namespace GameDb.Desktop.Pages.Auth.ViewModels
{
    public class AuthViewModel : NavigationViewModelBase
    {
        public AuthViewModel()
        {
            SignInCommand = new DelegateCommand<object>(SignIn);
        }

        #region Properties

        private string _login;

        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }

        #endregion

        #region Methods

        private void Authenticate(string login, string password)
        {
            var authorizationService = ServiceLocator.Current.GetInstance<IAuthorizationService>();
            var identity = authorizationService.AuthenticateUser(login, password);
            ServiceLocator.Current.GetInstance<IIdentityService>().SignIn(identity);
        }

        /// <summary>
        /// Login and password can not be null or empty
        /// </summary>
        /// <returns>If login or password format is not valid returns false otherwise true</returns>
        private bool ValidateLoginPassword(string login, string password)
        {
            return !(string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(login));
        }

        #endregion

        public ICommand SignInCommand { get; }

        private void SignIn(object param)
        {
            if (!(param is PasswordBox passwordBox)) return;

            var password = passwordBox.Password;

            if (!ValidateLoginPassword(_login, password)) return;

            try
            {
                Authenticate(_login, password);
                Navigator.NavigateTo(PageNames.MainPage);
                Navigator.ClearNavigationJournal();
            }
            catch (UnauthorizedAccessException exception)
            {
                var logger = ServiceLocator.Current.GetInstance<ILoggerFacade>();
                logger.Log($"Sign in error: {exception.Message}", Category.Info, Priority.Medium);
                ServiceLocator.Current.GetInstance<ISimpleMessageBoxService>().
                               Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception exception)
            {
                ServiceLocator.Current.GetInstance<ISimpleMessageBoxService>().
                               Show($"An unexpected error.\n{exception.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region INavigation Aware

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
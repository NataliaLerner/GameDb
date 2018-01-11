using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GameDb.Base.Interfaces;
using GameDb.Desktop.Base;
using GameDb.Desktop.Base.BaseTypes;
using GameDb.Desktop.Base.Interfaces;
using Microsoft.Practices.ServiceLocation;
using Prism.Commands;
using Prism.Regions;

namespace GameDb.Desktop.Pages.Main.ViewModels
{
    public class MainPageViewModel : NavigationViewModelBase
    {
        public MainPageViewModel()
        {
            SignOutCommand = new DelegateCommand(SignOut);

            var userGroup = ServiceLocator.Current.GetInstance<IIdentityService>().Identity.UserEntityData.UserGroup;

            if (userGroup == 0)
            {
                _pages = new ObservableCollection<PageConfiguration>
                {
                    new PageConfiguration(PageNames.Account, "My account")
                    ,new PageConfiguration(PageNames.Users, "Users")
                };
            }
            else
            {
                _pages = new ObservableCollection<PageConfiguration>
                {
                    new PageConfiguration(PageNames.Account, "My account")
                };
            }
        }

        private PageConfiguration _selectedPage;

        public PageConfiguration SelectedPage
        {
            get => _selectedPage;
            set => SetProperty(ref _selectedPage, value);
        }

        private ObservableCollection<PageConfiguration> _pages;
        public ObservableCollection<PageConfiguration> Pages => _pages;

        #region Commands

        public ICommand SignOutCommand { get; }

        private void SignOut()
        {
            var msgService = ServiceLocator.Current.GetInstance<ISimpleMessageBoxService>();
            var msgResult = msgService.Show("Are you sure you want to signout?", "Achtung!!!!",
                                            MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (msgResult == MessageBoxResult.Yes)
            {
                var identityService = ServiceLocator.Current.GetInstance<IIdentityService>();
                identityService.SignOut();
                Navigator.NavigateTo(PageNames.AuthPage);
                Navigator.ClearNavigationJournal();
            }
        }

        #endregion

        #region INavigationAware

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            var identityService = ServiceLocator.Current.GetInstance<IIdentityService>();
            return identityService.Identity?.UserEntityData.Login != null;
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        #endregion
    }
}
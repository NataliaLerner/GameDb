using GameDb.Base.Interfaces;
using GameDb.Desktop.Base.BaseTypes;
using GameDb.Desktop.EntityVMs.User;
using Microsoft.Practices.ServiceLocation;
using Prism.Mvvm;
using Prism.Regions;

namespace GameDb.Desktop.Pages.Main.ViewModels
{
    public class AccountPageViewModel : NavigationViewModelBase
    {
        public AccountPageViewModel()
        {
            var data = ServiceLocator.Current.GetInstance<IIdentityService>().Identity.UserEntityData;
            _userEntityViewModel = new UserEntityViewModel(data);
        }

        private UserEntityViewModel _userEntityViewModel;
        public UserEntityViewModel UserEntityViewModel => _userEntityViewModel;

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
    }
}
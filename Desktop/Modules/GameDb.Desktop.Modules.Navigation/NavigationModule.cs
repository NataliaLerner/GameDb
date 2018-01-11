using System;
using GameDb.Desktop.Base;
using GameDb.Desktop.Base.Interfaces;
using GameDb.Desktop.Pages.Auth.Views;
using GameDb.Desktop.Pages.Main.ViewModels;
using GameDb.Desktop.Pages.Main.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

namespace GameDb.Desktop.Modules.Navigation
{
    public class NavigationModule : INavigationModule, IModule
    {
        public NavigationModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void NavigateTo(PageNames pageName, string regionName = RegionNames.MainRegion)
        {
            NavigateTo(pageName, null, regionName);
        }

        public void NavigateTo(PageNames pageName, NavigationParameters parameters, string regionName = RegionNames.MainRegion)
        {

            if (_regionManager.Regions[regionName].GetView(pageName.ToString()) == null)
            {
                //Helpers.InitializeViewHelper.RegisterViewForNavigationWithinContainer(pageName, _container);
            }

            if (parameters == null)
            {
                parameters = new NavigationParameters();
            }
            _regionManager.RequestNavigate(regionName, pageName.ToString(), CheckForError, parameters);
        }

        public void ClearNavigationJournal()
        {
            var mainRegion = _regionManager.Regions[RegionNames.MainRegion];
            mainRegion.NavigationService.Journal.Clear();
        }

        void CheckForError(NavigationResult nr)
        {
            if (nr.Result == false)
            {
                throw new Exception(nr.Error.Message);
            }
        }

        public void NavigateForward()
        {
            _regionManager.Regions[RegionNames.MainRegion].NavigationService.Journal.GoForward();
        }

        public bool CanNavigateForward()
        {
            return _regionManager.Regions[RegionNames.MainRegion].NavigationService.Journal.CanGoForward;
        }

        public void NavigateBack()
        {
            _regionManager.Regions[RegionNames.MainRegion].NavigationService.Journal.GoBack();

        }

        public bool CanNavigateBack()
        {
            return _regionManager.Regions[RegionNames.MainRegion].NavigationService.Journal.CanGoBack;
        }

        public void Initialize()
        {
            _container.RegisterTypeForNavigation<AuthView>(PageNames.AuthPage.ToString());
            _container.RegisterTypeForNavigation<AuthSignUpView>(PageNames.AuthPageSignUp.ToString());
            _container.RegisterTypeForNavigation<MainPageView>(PageNames.MainPage.ToString());
            _container.RegisterTypeForNavigation<AccountPageView>(PageNames.Account.ToString());
            _container.RegisterTypeForNavigation<UsersCollectionPageView>(PageNames.Users.ToString());

            _regionManager.RequestNavigate(RegionNames.MainRegion, PageNames.AuthPage.ToString());
        }

        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;
    }
}
using System.Windows.Input;
using GameDb.Desktop.Base.Interfaces;
using Microsoft.Practices.ServiceLocation;
using Prism.Commands;
using Prism.Regions;

namespace GameDb.Desktop.Base.BaseTypes
{
    public abstract class NavigationViewModelBase : ViewModelBase, INavigationAware, IRegionMemberLifetime
    {
        protected INavigationModule Navigator => ServiceLocator.Current.GetInstance<INavigationModule>();

        public abstract void OnNavigatedTo(NavigationContext navigationContext);

        public abstract bool IsNavigationTarget(NavigationContext navigationContext);

        public abstract void OnNavigatedFrom(NavigationContext navigationContext);

        public virtual bool KeepAlive => false;

        private ICommand _navigateCommand;

        public ICommand NavigateCommand => _navigateCommand ?? (_navigateCommand = new DelegateCommand<object>(Navigate, CanNavigate));

        protected virtual bool CanNavigate(object arg)
        {
            return true;
        }

        protected virtual void Navigate(object param)
        {
            if (param != null)
            {
                var pg = (PageNames)param;
                Navigator.NavigateTo(pg);
            }
        }
    }
}
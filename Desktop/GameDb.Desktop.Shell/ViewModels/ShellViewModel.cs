using System.Collections.ObjectModel;
using System.Windows.Input;
using GameDb.Desktop.Base;
using GameDb.Desktop.Base.BaseTypes;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace GameDb.Desktop.Shell.ViewModels
{
    public class ShellViewModel : NavigationViewModelBase
    {
        public ShellViewModel()
        {
            NavigateBackCommand = new DelegateCommand(NavigateBack);
            NavigateForwardCommand = new DelegateCommand(NavigateForward);
        }

        #region Commands

        public DelegateCommand NavigateForwardCommand { get; private set; }

        private void NavigateForward()
        {
            Navigator.NavigateForward();
        }

        private bool CanNavigateForward()
        {
            return Navigator.CanNavigateForward();
        }


        public ICommand NavigateBackCommand { get; private set; }

        private void NavigateBack()
        {
            Navigator.NavigateBack();
        }

        private bool CanNavigateBack()
        {
            return Navigator.CanNavigateBack();
        }

        #endregion

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
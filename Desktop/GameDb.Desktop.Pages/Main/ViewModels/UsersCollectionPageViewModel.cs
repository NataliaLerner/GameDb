using System;
using System.Windows.Input;
using GameDb.Desktop.Base;
using GameDb.Desktop.Base.BaseTypes;
using GameDb.Desktop.EntityVMs.User;
using Prism.Commands;
using Prism.Regions;

namespace GameDb.Desktop.Pages.Main.ViewModels
{
    public class UsersCollectionPageViewModel : NavigationViewModelBase
    {
        public UsersCollectionPageViewModel()
        {
            UserSelectedCommand = new DelegateCommand<object>(UserSelected);
            UserEntityCollectionViewModel = new UserEntityCollectionViewModel();
        }

        public UserEntityCollectionViewModel UserEntityCollectionViewModel { get; }


        #region Commands

        public ICommand UserSelectedCommand { get; }

        private void UserSelected(object param)
        {
            var dataGridSelectedItem = param as UserEntityViewModel;

            if (dataGridSelectedItem != null)
            {
                UserEntityCollectionViewModel.SelectedItem = dataGridSelectedItem;
            }
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
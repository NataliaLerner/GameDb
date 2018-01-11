using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using GameDb.DAL.Entities;
using GameDb.DAL.Repositories;
using GameDb.Desktop.Base.BaseTypes;
using GameDb.Desktop.Base.Interfaces;
using Microsoft.Practices.ServiceLocation;
using Prism.Logging;

namespace GameDb.Desktop.EntityVMs.User
{
    public sealed class UserEntityCollectionViewModel : EntityCollectionViewModelBase<UserEntityViewModel>
    {
        public UserEntityCollectionViewModel()
        {
            Initialize();
        }

        protected override void InitializeFilters()
        {
            Filters = new Dictionary<string, object>
            {
                {"UserId", null },
                {"FirstName", null},
                {"LastName", null},
                {"MiddleName", null},
                {"Login", null}
            };
        }

        protected override void InitializeCollection()
        {
            var userDataCollection = LoadFromDb();
            var userViewModels = from userData in userDataCollection
                                 select new UserEntityViewModel(userData);
            Collection = new ObservableCollection<UserEntityViewModel>(userViewModels);
        }

        private IEnumerable<UserEntity> LoadFromDb()
        {
            try
            {
                var rep = new UserRepository();
                var currentUserId = GetCurrentUserId();


                var userModels = rep.GetUsers(null, null, null, null, currentUserId);
                return userModels;
            }
            catch (Exception exception)
            {
                var logger = ServiceLocator.Current.GetInstance<ILoggerFacade>();
                logger.Log($"An unexpected error in AuthPageSignInViewModel: {exception.Message}", Category.Exception, Priority.Medium);
                ServiceLocator.Current.GetInstance<ISimpleMessageBoxService>().
                               Show("An unexpected error. Inspect logs", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<UserEntity>();
            }
        }
    }
}
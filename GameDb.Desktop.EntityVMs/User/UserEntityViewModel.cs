using System;
using System.Data.SqlClient;
using System.Windows;
using GameDb.DAL.Entities;
using GameDb.DAL.Repositories;
using GameDb.Desktop.Base.BaseTypes;
using GameDb.Desktop.Base.Interfaces;
using Microsoft.Practices.ServiceLocation;
using Prism.Logging;

namespace GameDb.Desktop.EntityVMs.User
{
    public class UserEntityViewModel : EntityViewModelBase
    {
        public UserEntityViewModel(UserEntity data)
        {
            if (data == null)
            {
                data = new UserEntity();
                MarkAsNew();
            }
            else
            {
                MarkAsClean();
            }

            _data = data;
        }

        private readonly UserEntity _data;

        #region Properties

        public string Login
        {
            get => _data.Login;
            set => SetEntityProperty(ref _data.Login, value);
        }

        public string FirstName
        {
            get => _data.FirstName;
            set => SetEntityProperty(ref _data.FirstName, value);
        }

        public string LastName
        {
            get => _data.LastName;
            set => SetEntityProperty(ref _data.LastName, value);
        }

        public string Email
        {
            get => _data.Email;
            set => SetEntityProperty(ref _data.Email, value);
        }

        public string UserGroupDescr
        {
            get => _data.UserGroupDescr;
            set => SetEntityProperty(ref _data.UserGroupDescr, value);
        }

        #endregion

        protected override void SaveEntity()
        {
            try
            {
                var urep = new UserRepository();
                var curUserId = GetCurrentUserId();
                urep.Update(_data, curUserId);
                MarkAsClean();
            }
            catch (SqlException exception)
            {
                var logger = ServiceLocator.Current.GetInstance<ILoggerFacade>();
                logger.Log($"Error in UserEntityViewModel: {exception.Message}", Category.Exception, Priority.Medium);
                ServiceLocator.Current.GetInstance<ISimpleMessageBoxService>().
                               Show($"Error\n{exception.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception exception)
            {
                var logger = ServiceLocator.Current.GetInstance<ILoggerFacade>();
                logger.Log($"An unexpected error in UserEntityViewModel: {exception.Message}", Category.Exception, Priority.Medium);
                ServiceLocator.Current.GetInstance<ISimpleMessageBoxService>().
                               Show($"An unexpected error\n{exception.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
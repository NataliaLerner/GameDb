using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Prism.Commands;

namespace GameDb.Desktop.Base.BaseTypes
{
    public abstract class EntityCollectionViewModelBase<T> : StateTrackableViewModelBase
    {
        #region Constructors

        public EntityCollectionViewModelBase()
        {
            ApplyFilterCommand = new DelegateCommand(ApplyFilter, CanApplyFilter).ObservesProperty(() => Filters);
        }

        #endregion

        #region Properties

        private ObservableCollection<T> _collection;

        public virtual ObservableCollection<T> Collection
        {
            get => _collection;
            set => SetProperty(ref _collection, value);
        }

        private T _selectedItem;

        public virtual T SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        #endregion //Properties 

        #region Filters

        protected Dictionary<string, object> _filters;

        public Dictionary<string, object> Filters
        {
            get => _filters;
            set => SetProperty(ref _filters, value);
        }

        protected virtual void InitializeFilters()
        {
            //EXAMPLE
            //_filters = new Dictionary<string, object>
            //{
            //    {"UserId", null },
            //    {"FirstName", null},
            //    {"LastName", null},
            //    {"MiddleName", null},
            //    {"Login", ""}
            //};
        }

        public virtual ICommand ApplyFilterCommand { get; }

        private void ApplyFilter()
        {
            InitializeCollection();
        }

        private bool CanApplyFilter()
        {
            return true;
        }

        #endregion //Filters    

        #region Methods

        protected virtual void Initialize()
        {
            InitializeFilters();
            InitializeCollection();
        }

        protected abstract void InitializeCollection();

        private IEnumerable<T> LoadFromDb()
        {
            return new List<T>();
        }
        //private IEnumerable<UserData> LoadFromDb()
        //{
        //    try
        //    {
        //        var rep = new UserRepository();
        //        var userModels = rep.GetAll(Filters["UserId"], Filters["FirstName"], Filters["LastName"], Filters["MiddleName"], Filters["Login"], 1);
        //        return userModels;
        //    }
        //    catch (Exception exception)
        //    {
        //        MessageBox.Show(exception.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        //        return new List<UserData>();
        //    }
        //}

        #endregion
    }
}
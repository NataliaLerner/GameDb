using System.Windows.Input;
using Prism.Commands;

namespace GameDb.Desktop.Base.BaseTypes
{
    public abstract class EntityViewModelBase : StateTrackableViewModelBase
    {
        public EntityViewModelBase()
        {
            CreateEntityCommand = new DelegateCommand(CreateEntity, CanCreateEntity).ObservesProperty(() => State);
            SaveEntityCommand = new DelegateCommand(SaveEntity, CanSaveEntity).ObservesProperty(() => State);
            DeleteEntityCommand = new DelegateCommand(DeleteEntity, CanDeleteEntity).ObservesProperty(() => State);
        }

        #region Commands

        public virtual ICommand CreateEntityCommand { get; }

        protected virtual void CreateEntity()
        {

        }

        protected virtual bool CanCreateEntity()
        {
            return true;
        }

        public virtual ICommand SaveEntityCommand { get; }

        protected virtual void SaveEntity()
        {

        }

        protected virtual bool CanSaveEntity()
        {
            return State == ObjectStates.Dirty || State == ObjectStates.New;
        }

        public virtual ICommand DeleteEntityCommand { get; }

        protected virtual void DeleteEntity()
        {

        }

        protected virtual bool CanDeleteEntity()
        {
            return State == ObjectStates.Clean;
        }

        #endregion
    }
}
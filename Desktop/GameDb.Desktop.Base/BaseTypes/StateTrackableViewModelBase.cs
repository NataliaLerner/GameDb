using System.Runtime.CompilerServices;
using GameDb.Base.Interfaces;
using GameDb.Desktop.Base.Interfaces;
using Microsoft.Practices.ServiceLocation;

namespace GameDb.Desktop.Base.BaseTypes
{
    public class StateTrackableViewModelBase : ViewModelBase, IStateTracking
    {
        protected virtual void SetEntityProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (SetProperty(ref storage, value, propertyName))
            {
                MarkAsDirty();
            }
        }

        protected virtual int GetCurrentUserId()
        {
            var identityService = ServiceLocator.Current.GetInstance<IIdentityService>();
            var id = identityService.Identity.UserEntityData.UserId;
            return id;
        }

        #region IStateTracking

        private ObjectStates _state;

        public ObjectStates State
        {
            get => _state;
            private set => SetProperty(ref _state, value);
        }

        public void MarkAsNew()
        {
            State = ObjectStates.New;
        }

        public void MarkAsDirty()
        {
            State = ObjectStates.Dirty;
        }

        public void MarkAsClean()
        {
            State = ObjectStates.Clean;
        }

        public void MarkAsDeleted()
        {
            State = ObjectStates.Deleted;
        }

        #endregion
    }
}
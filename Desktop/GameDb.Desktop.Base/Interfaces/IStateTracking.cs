namespace GameDb.Desktop.Base.Interfaces
{
    public interface IStateTracking
    {
        ObjectStates State { get; }
        void MarkAsNew();
        void MarkAsDirty();
        void MarkAsClean();
        void MarkAsDeleted();
    }
}
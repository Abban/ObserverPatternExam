public interface IObservableState
{
    void StartTransaction();
    void SetChanged(IObservableStateProperty property);
    void EndTransaction();
}
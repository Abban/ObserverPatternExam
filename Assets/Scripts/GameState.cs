public class GameState : IStateObserverNotifier
{
    private readonly ObservableStateBroker _stateBroker;
    public void NotifyObservers() => _stateBroker.NotifyObservers();
    public IObservableStateProperty<int> Coins { get; }
    public IObservableStateProperty<int> Stars { get; }


    public GameState(
        int coins,
        int stars)
    {
        _stateBroker = new ObservableStateBroker();
        
        Coins = new ObservableStateProperty<int>(_stateBroker, coins);
        Stars = new ObservableStateProperty<int>(_stateBroker, stars);
    }
}
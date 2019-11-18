public class GameState : IStateObserverNotifier
{
    private readonly IStateObserverNotifier _stateNotifier;
    public void NotifyObservers() => _stateNotifier.NotifyObservers();
    public IObservableStateProperty<int> Coins { get; }
    public IObservableStateProperty<int> Stars { get; }


    public GameState(
        IStateObserverNotifier stateNotifier,
        IObservableStateProperty<int> coins,
        IObservableStateProperty<int> stars)
    {
        _stateNotifier = stateNotifier;
        Coins = coins;
        Stars = stars;
    }
}
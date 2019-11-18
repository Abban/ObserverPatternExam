public class GameStateService
{
    private static readonly GameStateService _instance = new GameStateService();

    public static GameStateService Get()
    {
        return _instance;
    }

    public GameState State { get; private set; }
    public IStateObserverNotifier Notifier { get; private set; }

    public void Init(int coins, int stars)
    {
        Notifier = new ObservableStateBroker();
        var broker = Notifier as IStatePropertyBroker;
        
        var coinsProperty = new ObservableStateProperty<int>(broker, coins);
        var starsProperty = new ObservableStateProperty<int>(broker, stars);
        
        State = new GameState(
            coinsProperty,
            starsProperty
        );
    }
}
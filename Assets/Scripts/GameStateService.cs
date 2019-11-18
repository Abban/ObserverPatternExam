public class GameStateService
{
    private static readonly GameStateService _instance = new GameStateService();

    public static GameStateService Get()
    {
        return _instance;
    }

    public GameState State { get; private set; }

    public void Init(int coins, int stars)
    {
        var stateBroker = new ObservableStateBroker();
        var coinsProperty = new ObservableStateProperty<int>(stateBroker, coins);
        var starsProperty = new ObservableStateProperty<int>(stateBroker, stars);
        
        State = new GameState(
            stateBroker,
            coinsProperty,
            starsProperty
        );
    }
}
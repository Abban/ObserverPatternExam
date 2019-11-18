public class GameState : ObservableState
{
    public IObservableStateProperty<int> Coins { get; }
    public IObservableStateProperty<int> Stars { get; }


    public GameState(
        int coins,
        int stars)
    {
        Coins = new ObservableStateProperty<int>(this, coins);
        Stars = new ObservableStateProperty<int>(this, stars);
    }
}
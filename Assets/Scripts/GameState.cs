public class GameState
{
    public IObservableStateProperty<int> Coins { get; }
    public IObservableStateProperty<int> Stars { get; }


    public GameState(
        IObservableStateProperty<int> coins,
        IObservableStateProperty<int> stars)
    {
        Coins = coins;
        Stars = stars;
    }
}
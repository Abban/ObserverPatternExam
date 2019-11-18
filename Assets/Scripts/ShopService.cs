public class ShopService
{
    private static readonly ShopService _instance = new ShopService();

    public static ShopService Get()
    {
        return _instance;
    }
    
    public void BuyStars(int stars)
    {
        var gameStateService = GameStateService.Get();
        
        gameStateService.State.Stars.Value += stars;
        gameStateService.State.NotifyObservers();
    }

    public void BuyStars(int stars, int forCoins)
    {
        GameStateService.Get().State.Stars.Value += stars;
        UseCoins(forCoins);
    }

    public void UseCoins(int coins)
    {
        var gameStateService = GameStateService.Get();
        
        gameStateService.State.Coins.Value -= coins;
        gameStateService.State.NotifyObservers();
    }
}
using NUnit.Framework;

namespace Tests
{
    public class TestSuite
    {
        [Test]
        public void CanInitGameState()
        {
            var gameStateService = GameStateService.Get();
            gameStateService.Init(10,0);

            var gameState = gameStateService.State;
        
            Assert.That(gameState.Coins.Value, Is.EqualTo(10));
            Assert.That(gameState.Stars.Value, Is.EqualTo(0));
        }

        [Test]
        public void CanObserveGameStateChanges()
        {
            var gameStateService = GameStateService.Get();
            gameStateService.Init(10,0);

            var gameState = gameStateService.State;
            var stateObserverCalled = false;
            gameState.Coins.Action += () =>
            {
                stateObserverCalled = true;
                Assert.That(gameState.Coins.Value, Is.EqualTo(8));
            };
            
            ShopService.Get().UseCoins(2);

            Assert.That(stateObserverCalled, "Observer not called");
        }
    
        [Test]
        public void CanObserveConsistentGameStateChanges()
        {
            var gameStateService = GameStateService.Get();
            gameStateService.Init(10,0);

            var stateObserverCalled = false;
            void StateValidator()
            {
                stateObserverCalled = true;
                var gameState = gameStateService.State;
                Assert.That(gameState.Stars.Value, Is.EqualTo(1));
                Assert.That(gameState.Coins.Value, Is.EqualTo(9));
            }

            gameStateService.State.Coins.Action += StateValidator;
            gameStateService.State.Stars.Action += StateValidator;

            var shopService = ShopService.Get();
            shopService.BuyStars(1, 1);
            
            Assert.That(stateObserverCalled, "Observer not called");
        }

        [Test]
        public void ObservableStateProperty_OnChangeValue_NotifiesObservableState()
        {
            var observableState = new MockObservableStateBroker();
            var coins = new ObservableStateProperty<int>(observableState, 10);
            coins.Value -= 2;
            
            Assert.That(observableState.Changed);
        }
        
        
        
        private class MockObservableStateBroker : IObservableStateBroker
        {
            public bool Changed { get; private set; }

            public void SetChanged(IObservableStateProperty property)
            {
                Changed = true;
            }
        }
    }
}
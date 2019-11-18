using NUnit.Framework;

namespace Tests
{
    public class TestSuite
    {
        [Test]
        public void CanInitGameState()
        {
            var gameStateService = GameStateService.Get();
            gameStateService.Init(10, 0);

            var gameState = gameStateService.State;

            Assert.That(gameState.Coins.Value, Is.EqualTo(10));
            Assert.That(gameState.Stars.Value, Is.EqualTo(0));
        }


        [Test]
        public void CanObserveGameStateChanges()
        {
            var gameStateService = GameStateService.Get();
            gameStateService.Init(10, 0);

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
            gameStateService.Init(10, 0);

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
        public void ObservableStateBroker_OnNotifyObservers_SendsSingleNotificationPerObserver()
        {
            var gameStateService = GameStateService.Get();
            gameStateService.Init(10, 0);

            var stateObserverCalled = false;
            var callCount = 0;
            
            void StateValidator()
            {
                stateObserverCalled = true;
                callCount++;
            }

            gameStateService.State.Coins.Action += StateValidator;
            gameStateService.State.Stars.Action += StateValidator;

            gameStateService.State.Coins.Value += 2;
            gameStateService.State.Stars.Value += 2;
            
            gameStateService.Notifier.NotifyObservers();

            Assert.That(stateObserverCalled, "Observer not called");
            Assert.That(callCount == 1, "Observer called too many times");
        }


        [Test]
        public void ObservableStateProperty_OnChangeValue_NotifiesPropertyStateBroker()
        {
            var stateBroker = new MockStatePropertyBroker();
            var coins = new ObservableStateProperty<int>(stateBroker, 10);
            coins.Value -= 2;

            Assert.That(stateBroker.Changed, "Property did not notify broker of change");
        }


        private class MockStatePropertyBroker : IStatePropertyBroker
        {
            public bool Changed { get; private set; }

            public void SetChanged(IObservableStateProperty property)
            {
                Changed = true;
            }
        }
    }
}
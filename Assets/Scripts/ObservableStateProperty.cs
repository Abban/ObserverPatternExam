using System;

public class ObservableStateProperty<T> : IObservableStateProperty<T>
{
    private readonly IObservableStateBroker _observableStateBroker;

    private T _value;

    public T Value
    {
        get => _value;
        set
        {
            var oldValue = _value;
            _value = value;

            if (!Equals(value, oldValue))
            {
                _observableStateBroker.SetChanged(this);
            }
        }
    }
    
    public Action Action { get; set; } = () => { };

    public ObservableStateProperty(
        IObservableStateBroker observableStateBroker,
        T startValue)
    {
        _observableStateBroker = observableStateBroker;
        _value = startValue;
    }
}
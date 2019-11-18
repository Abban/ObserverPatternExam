using System;

public class ObservableStateProperty<T> : IObservableStateProperty<T>
{
    private readonly IObservableState _observer;

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
                _observer.SetChanged(this);
            }
        }
    }
    
    public Action Action { get; set; } = () => { };

    public ObservableStateProperty(
        IObservableState observer,
        T startValue)
    {
        _observer = observer;
        _value = startValue;
    }
}
using System;

public interface IObservableStateProperty
{
    Action Action { get; set; }
}

public interface IObservableStateProperty<T> : IObservableStateProperty
{
    T Value { get; set; }
}
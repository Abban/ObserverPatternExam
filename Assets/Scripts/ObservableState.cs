using System.Collections.Generic;

public abstract class ObservableState : IObservableStateBroker
{
    protected List<IObservableStateProperty> ChangedProperties = new List<IObservableStateProperty>();


    public void SetChanged(IObservableStateProperty property)
    {
        ChangedProperties.Add(property);
    }


    public void NotifyObservers()
    {
        foreach (var property in ChangedProperties)
        {
            property.Action();
        }
    }
}
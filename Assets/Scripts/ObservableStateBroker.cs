using System;
using System.Collections.Generic;

public abstract class ObservableStateBroker : IPropertyStateBroker
{
    protected List<IObservableStateProperty> ChangedProperties = new List<IObservableStateProperty>();


    /// <inheritdoc />
    public void SetChanged(IObservableStateProperty property)
    {
        ChangedProperties.Add(property);
    }

    
    /// <summary>
    /// Loops changed values and their delegates and manually notifies each delegate one time 
    /// </summary>
    public void NotifyObservers()
    {
        var called = new List<Delegate>();
        
        // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
        foreach (var property in ChangedProperties)
        {
            var delegates = property.Action.GetInvocationList();

            foreach (var @delegate in delegates)
            {
                if (called.Contains(@delegate)) continue;
                
                @delegate.DynamicInvoke();
                called.Add(@delegate);
            }
        }
        
        ChangedProperties.Clear();
    }
}
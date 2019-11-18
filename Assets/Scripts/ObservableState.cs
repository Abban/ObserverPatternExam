using System.Collections.Generic;
using UnityEngine;

public abstract class ObservableState : IObservableState
{
    protected List<IObservableStateProperty> ChangedProperties = new List<IObservableStateProperty>();


    public void StartTransaction()
    {
        // ChangedProperties.Clear();
    }


    public void SetChanged(IObservableStateProperty property)
    {
        ChangedProperties.Add(property);
        
//        Debug.Log("Adding Changed");
//        Debug.Log(ChangedProperties.Count);
    }


    public void EndTransaction()
    {
//        Debug.Log("Ending Transaction");
//        Debug.Log(ChangedProperties.Count);
        foreach (var property in ChangedProperties)
        {
            property.Action();
        }
    }
}
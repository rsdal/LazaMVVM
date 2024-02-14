using System;
using UnityEngine;

[Serializable]
public class ViewModelField<T> : IViewModelField
{
    [SerializeField]
    private T propertyValue;
    
    public T Value
    {
        get => propertyValue;
        set
        {
            propertyValue = value;
            OnValueChanged?.Invoke(propertyValue);
        }
    }

    public Action<object> OnValueChanged { get; set; }
    
    public object GetObject => propertyValue;
}
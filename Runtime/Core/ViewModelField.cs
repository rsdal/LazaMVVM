using System;
using System.Collections.Generic;
using UnityEngine;

namespace LazaMVVM.Runtime.Core
{
    [Serializable]
    public class ViewModelField<T> : IViewModelField
    {
        [SerializeField]
        protected T propertyValue;
    
        public T Value
        {
            get => propertyValue;
            set
            {
                if (EqualityComparer<T>.Default.Equals(propertyValue, value)) return;
                
                propertyValue = value;
                OnValueChanged?.Invoke(propertyValue);
            }
        }

        public Action<object> OnValueChanged { get; set; }
    
        public object GetObject => Value;

        public void SetObject(object value)
        {
            propertyValue = (T)value;
        }
    }
}

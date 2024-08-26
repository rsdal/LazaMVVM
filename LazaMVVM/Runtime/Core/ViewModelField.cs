using System;
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
                propertyValue = value;
                OnValueChanged?.Invoke(propertyValue);
            }
        }

        public Action<object> OnValueChanged { get; set; }
    
        public object GetObject => Value;
    }
}
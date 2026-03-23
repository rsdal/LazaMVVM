using System;
using System.Collections.Generic;
using UnityEngine;

namespace LazaMVVM.Runtime.Core
{
    [Serializable]
    public class ViewModelField<T> : IViewModelField, ISerializationCallbackReceiver
    {
        [SerializeField]
        protected T propertyValue;
    
        private T _lastInspectorValue;
        
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
        
        void ISerializationCallbackReceiver.OnBeforeSerialize() { }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            if (EqualityComparer<T>.Default.Equals(propertyValue, _lastInspectorValue)) return;
            
            _lastInspectorValue = propertyValue;
                
#if UNITY_EDITOR
            UnityEditor.EditorApplication.delayCall += NotifyFromInspector;
#endif
        }

#if UNITY_EDITOR
        private void NotifyFromInspector()
        {
            UnityEditor.EditorApplication.delayCall -= NotifyFromInspector;
            OnValueChanged?.Invoke(propertyValue);
        }
#endif
        
    }
}

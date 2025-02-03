
using UnityEngine;

namespace LazaMVVM.Runtime.Core
{
    public abstract class BaseFieldBind<T> : BaseBind
    {
        protected IViewModelField field;

        protected override void Initialize()
        {
            base.Initialize();

            if (BindInfo.RuntimeValue == null)
            {
                Debug.LogError($"View model is null on \n" +
                               $"GameObject: {gameObject.name}");
                
                return;
            }
            
            bool isSuccess = BindInfo.RuntimeValue.GetFieldByName(BindInfo.Field, out field);
        
            if (!isSuccess)
            {
                Debug.LogError($"Wasn't possible to find a field with: \n " +
                               $"name: {BindInfo.Field} \n " +
                               $"viewmodel: {BindInfo.RuntimeValue.GetType()} \n " +
                               $"GameObject: {gameObject.name}");
            
                return;
            }
        
            OnInternalValueChanged(field.GetObject);
        
            field.OnValueChanged += OnInternalValueChanged;
        }

        private void OnInternalValueChanged(object newValue)
        { 
            OnValueChanged((T)newValue);
        }

        private void OnDestroy()
        {
            if (field == null)
            {
                return;
            }
        
            field.OnValueChanged -= OnInternalValueChanged;
        }

        protected abstract void OnValueChanged(T newValue);
    }
}

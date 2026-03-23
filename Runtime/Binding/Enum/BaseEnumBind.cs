using System;
using LazaMVVM.Runtime.Core;
using UnityEngine;

namespace LazaMVVM.Runtime.Binding
{
    public abstract class BaseEnumBind : BaseFieldBind<Enum>
    {
        [SerializeField]
        private EnumSelector enumSelector;
        
        protected override void OnValueChanged(Enum newValue)
        {
            bool isValueEqual = enumSelector.SelectedEnumValue == newValue.ToString();
            
            OnChanged(newValue, isValueEqual);
        }

        protected abstract void OnChanged(Enum newValue, bool isEqual);
    }
}
using System;
using System.Linq;
using UnityEngine;

namespace LazaMVVM.Runtime
{
    [Serializable]
    public class EnumSelector
    {
        [SerializeField] private string selectedEnumType;
        [SerializeField] private string selectedEnumValue;

        public Type SelectedType => string.IsNullOrEmpty(selectedEnumType) ? null : Type.GetType(selectedEnumType);
    
        public string SelectedEnumValue => selectedEnumValue;
    
        public void SetEnumType(Type type)
        {
            selectedEnumType = type?.AssemblyQualifiedName;
            selectedEnumValue = type?.IsEnum == true ? Enum.GetNames(type).FirstOrDefault() : "";
        }

        public void SetEnumValue(string value)
        {
            if (SelectedType != null && Enum.IsDefined(SelectedType, value))
            {
                selectedEnumValue = value;
            }
        }
    }
}

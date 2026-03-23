using System;
using LazaMVVM.Runtime.Attributes;
using UnityEngine;

namespace LazaMVVM.Runtime.Binding
{
    [BindFilter(typeof(Enum))]
    public class EnumActiveBinding : BaseEnumBind
    {
        [SerializeField] private Targets Target;
        [SerializeField] private GameObject TargetObject;

        [SerializeField]
        private bool EnableIfEqual;
        
        protected override void OnChanged(Enum newValue, bool isEqual)
        {
            if (Target == Targets.Target)
            {
                TargetObject.SetActive(EnableIfEqual && isEqual);
                return;
            }
            
            gameObject.SetActive(EnableIfEqual && isEqual);
        }
    }
}
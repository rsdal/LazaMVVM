using System;
using LazaMVVM.Runtime.Attributes;
using LazaMVVM.Runtime.Core;
using UnityEngine;

namespace LazaMVVM.Runtime.Binding
{
    [BindFilter(typeof(bool))]
    public class ActiveBinding : BaseFieldBind<bool>
    {
        [SerializeField] 
        private bool Invert;
        [SerializeField]
        private Targets Target;
        [SerializeField]
        private GameObject TargetObject;
        
        protected override void OnValueChanged(bool newValue)
        {
            GameObject correctGameObject = Target == Targets.Target ? TargetObject : gameObject;
            
            correctGameObject.SetActive(Invert ? !newValue : newValue);
        }
    }
}
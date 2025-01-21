using LazaMVVM.Runtime.Attributes;
using LazaMVVM.Runtime.Core;
using UnityEngine;

namespace LazaMVVM.Runtime.Binding
{
    [BindFilter(typeof(bool))]
    public class ActiveBinding : BaseFieldBind<bool>
    {
        [SerializeField]
        private Targets Target;
        [SerializeField]
        private GameObject TargetObject;
        
        
        protected override void OnValueChanged(bool newValue)
        {
            if (Target == Targets.Target)
            {
                TargetObject.SetActive(newValue);
                return;
            }
            
            gameObject.SetActive(newValue);
        }
    }

    public enum Targets
    {
        Self,
        Target
    }
}

using LazaMVVM.Runtime.Attributes;
using LazaMVVM.Runtime.Core;
using UnityEngine;

namespace LazaMVVM.Runtime.Binding
{
    [BindFilter(typeof(string), typeof(char), typeof(bool), typeof(int), typeof(float))]
    [RequireComponent(typeof(Transform))]
    public class ChangeTransformSibling : BaseFieldBind<object>
    {
        public bool SetAsFirst;
        
        protected override void OnValueChanged(object newValue)
        {
            if (SetAsFirst)
            {
                transform.SetAsFirstSibling();
                return;
            }
            
            transform.SetAsLastSibling();
        }
    }
}
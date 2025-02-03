using System.Reflection;
using LazaMVVM.Runtime.Attributes;
using LazaMVVM.Runtime.Core;
using UnityEngine;
using UnityEngine.UI;

namespace LazaMVVM.Runtime.Binding
{
    [CommandBindFilter]
    [RequireComponent(typeof(Button))]
    public class CommandBind : BaseBind
    { 
        private MethodInfo methodInfo;
        private Button _button;

        protected override void Initialize()
        {
            base.Initialize();

            if (BindInfo.RuntimeValue == null)
            {
                Debug.LogError($"View model is null on \n" +
                               $"GameObject: {gameObject.name}");
                
                return;
            }
            
            bool isSuccess = BindInfo.RuntimeValue.GetMethodByName(BindInfo.Field, out methodInfo);
        
            if (!isSuccess)
            {
                Debug.LogError($"Wasn't possible to find a method with: \n " +
                               $"name: {BindInfo.Field} \n " +
                               $"viewmodel: {BindInfo.RuntimeValue.GetType()} \n " +
                               $"GameObject: {gameObject.name}");
                return;
            }
        
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnInvoke);
        }

        private void OnDestroy()
        {
            _button?.onClick.RemoveListener(OnInvoke);
        }

        private void OnInvoke()
        {
            methodInfo.Invoke(BindInfo.RuntimeValue, null);
        }
    }
}

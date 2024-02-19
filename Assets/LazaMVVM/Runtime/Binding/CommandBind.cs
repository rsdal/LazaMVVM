using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

[CommandBindFilter]
[RequireComponent(typeof(Button))]
public class CommandBind : BaseBind
{ 
    private MethodInfo methodInfo;
    private Button _button;

    protected override void Initialize()
    {
        base.Initialize();
        
        bool isSuccess = BindInfo.baseViewModel.GetMethodByName(BindInfo.Field, out methodInfo);

        if (!isSuccess)
        {
            Debug.LogError($"Wasn't possible to find a method with: \n " +
                           $"name: {BindInfo.Field} \n " +
                           $"viewmodel: {BindInfo.baseViewModel.GetType()} \n " +
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
        methodInfo.Invoke(BindInfo.baseViewModel, null);
    }
}
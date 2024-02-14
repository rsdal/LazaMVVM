using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

[CommandBindFilter]
[RequireComponent(typeof(Button))]
public class CommandBind : BaseBind
{ 
    private MethodInfo methodInfo;
    private Button _button;

    private void Start()
    {
        methodInfo = BindInfo.baseViewModel.GetMethodByName(this, BindInfo.Field);
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnInvoke);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnInvoke);
    }

    private void OnInvoke()
    {
        methodInfo.Invoke(BindInfo.baseViewModel, null);
    }
}
using LazaMVVM.Runtime.Attributes;
using LazaMVVM.Runtime.Core;
using UnityEngine;
using UnityEngine.UI;

[BindFilter(typeof(bool))]
[RequireComponent(typeof(Button))]
public class ButtonInteractableBinding : BaseFieldBind<bool>
{
    private Button _button;
    
    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    protected override void OnValueChanged(bool newValue)
    {
        _button.interactable = newValue;   
    }
}
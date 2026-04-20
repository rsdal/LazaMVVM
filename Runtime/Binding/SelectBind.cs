using LazaMVVM.Runtime.Attributes;
using LazaMVVM.Runtime.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[BindFilter(typeof(bool))]
[RequireComponent(typeof(Button))]
public class SelectBind : BaseFieldBind<bool>
{
    Button button;
    
    private void Awake()
    {
        button = GetComponent<Button>();
    }

    protected override void OnValueChanged(bool newValue)
    {
        if (button == null)
        {
            Debug.LogWarning("You tried to set the value before the text initialized");
            return;
        }

        if (newValue)
        {
            EventSystem.current.SetSelectedGameObject(button.gameObject);
        }
    }
}
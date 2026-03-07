using LazaMVVM.Runtime.Attributes;
using LazaMVVM.Runtime.Core;
using TMPro;
using UnityEngine;

namespace LazaMVVM.Runtime.Binding
{
    [BindFilter(typeof(string), typeof(char), typeof(bool), typeof(int), typeof(float))]
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextMeshProFieldBind : BaseFieldBind<object>
    {
        private TextMeshProUGUI text;

        private void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        protected override void OnValueChanged(object newValue)
        {
            if (text == null)
            {
                Debug.LogWarning("You tried to set the value before the text initialized");
                return;
            }

            if (newValue == null)
            {
                Debug.LogWarning("You tried to set a null value");
                return;
            }
            
            text.SetText(newValue.ToString());
        }
    }
}
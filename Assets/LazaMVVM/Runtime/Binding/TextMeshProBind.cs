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
            text.SetText(newValue.ToString());
        }
    }
}
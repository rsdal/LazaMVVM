using System;
using LazaMVVM.Runtime.Attributes;
using TMPro;
using UnityEngine;

namespace LazaMVVM.Runtime.Binding
{
    [BindFilter(typeof(Enum))]
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextMeshProEnumBind : BaseEnumBind
    {
        private TextMeshProUGUI text;
        
        private void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
        }
        
        protected override void OnChanged(Enum newValue)
        {
            text.SetText(newValue.ToString());
        }
    }
}
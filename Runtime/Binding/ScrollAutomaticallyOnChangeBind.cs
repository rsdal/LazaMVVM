using System.Collections.Generic;
using LazaMVVM.Runtime.Attributes;
using LazaMVVM.Runtime.Core;
using UnityEngine;
using UnityEngine.UI;

namespace LazaMVVM.Runtime.Binding
{
    [BindFilter(typeof(IList<>))]
    [RequireComponent(typeof(ScrollRect))]
    public class ScrollAutomaticallyOnChangeBind : BaseFieldBind<List<IViewModel>>
    {
        public bool ToBottom = true;
        private ScrollRect _scrollRect;
        
        private void Awake()
        {
            _scrollRect = GetComponent<ScrollRect>();
        }
        
        protected override void OnValueChanged(List<IViewModel> newValue)
        {
            _scrollRect.verticalNormalizedPosition = ToBottom ? 0 : 1;
        }
    }
}
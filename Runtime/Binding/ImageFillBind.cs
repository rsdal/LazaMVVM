using LazaMVVM.Runtime.Attributes;
using LazaMVVM.Runtime.Core;
using UnityEngine;
using UnityEngine.UI;

namespace LazaMVVM.Runtime.Binding
{
    [BindFilter(typeof(float))]
    [RequireComponent(typeof(Image))]
    public class ImageFillBind : BaseFieldBind<float>
    {
        private Image image;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        protected override void OnValueChanged(float newValue)
        {
            image.fillAmount = newValue;
        }
    }
}
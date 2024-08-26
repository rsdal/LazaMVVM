using LazaMVVM.Runtime.Attributes;
using LazaMVVM.Runtime.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LazaMVVM.Runtime.Binding
{
    [BindFilter(typeof(Sprite))]
    [RequireComponent(typeof(Image))]
    public class ImageFieldBind : BaseFieldBind<Sprite>
    {
        private Image image;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        protected override void OnValueChanged(Sprite newValue)
        {
            image.sprite = newValue;
        }
    }
}
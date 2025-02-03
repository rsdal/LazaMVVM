using System;

namespace LazaMVVM.Runtime.Core
{
    public interface IViewModelField
    {
        Action<object> OnValueChanged { get; set; }

        object GetObject { get; }

        void SetObject(object value);
    }
}

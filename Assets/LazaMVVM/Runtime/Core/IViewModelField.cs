using System;

public interface IViewModelField
{
    Action<object> OnValueChanged { get; set; }

    object GetObject { get; }
}
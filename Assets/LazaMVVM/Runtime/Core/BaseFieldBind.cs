using System.Reflection;

public abstract class BaseFieldBind<T> : BaseBind
{
    private IViewModelField field;

    protected override void Initialize()
    {
        base.Initialize();
        
        field = BindInfo.baseViewModel.GetFieldByName(BindInfo.Field);
        OnInternalValueChanged(field.GetObject);
        field.OnValueChanged += OnInternalValueChanged;
    }

    private void OnInternalValueChanged(object newValue)
    { 
        OnValueChanged((T)newValue);
    }

    private void OnDestroy()
    {
        field.OnValueChanged -= OnInternalValueChanged;
    }

    protected abstract void OnValueChanged(T newValue);
}
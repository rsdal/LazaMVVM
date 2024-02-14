public abstract class BaseFieldBind<T> : BaseBind
{
    private IViewModelField field;

    private void Start()
    {
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
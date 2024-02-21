
using UnityEngine;

public abstract class BaseFieldBind<T> : BaseBind
{
    private IViewModelField field;

    protected override void Initialize()
    {
        base.Initialize();

        IViewModel viewModel = BindInfo.viewModel as IViewModel;
        
        bool isSuccess = viewModel.GetFieldByName(BindInfo.Field, out field);
        
        if (!isSuccess)
        {
            Debug.LogError($"Wasn't possible to find a field with: \n " +
                           $"name: {BindInfo.Field} \n " +
                           $"viewmodel: {BindInfo.viewModel.GetType()} \n " +
                           $"GameObject: {gameObject.name}");
            
            return;
        }
        
        OnInternalValueChanged(field.GetObject);
        
        field.OnValueChanged += OnInternalValueChanged;
    }

    private void OnInternalValueChanged(object newValue)
    { 
        OnValueChanged((T)newValue);
    }

    private void OnDestroy()
    {
        if (field == null)
        {
            return;
        }
        
        field.OnValueChanged -= OnInternalValueChanged;
    }

    protected abstract void OnValueChanged(T newValue);
}
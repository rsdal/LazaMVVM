using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class MonoBehaviourViewModel : MonoBehaviour, IViewModel
{
    private readonly ViewModel _viewModel = new ViewModel();
    
    public Dictionary<string, IViewModelField> GetFields()
    {
        return _viewModel.GetFields(this);
    }

    public bool GetFieldByName(string name, out IViewModelField field)
    {
        return _viewModel.GetFieldByName(this, name, out field);
    }

    public Dictionary<string, MethodInfo> GetMethods()
    {
        return _viewModel.GetMethods(this);
    }

    public bool GetMethodByName(string name, out MethodInfo method)
    {
        return _viewModel.GetMethodByName(this, name, out method);
    }

    public void RemoveItem()
    {
        Destroy(gameObject);
    }
}
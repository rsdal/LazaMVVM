using System;
using System.Reflection;
using UnityEngine;

public abstract class BaseBind : MonoBehaviour
{
    private void Start()
    {
        var listItem = BindInfo.baseViewModel.GetType().GetCustomAttribute<ListItem>();
        
        if (listItem != null)
        {
            Transform findTransform = transform;
            
            while (findTransform != null)
            {
                ViewModelProvider viewModelProvider = findTransform.GetComponent<ViewModelProvider>();
                
                if (viewModelProvider != null)
                {
                    BindInfo.baseViewModel = viewModelProvider.ViewModel;
                    break;
                }

                findTransform = transform.parent;
            }
        }

        Initialize();
    }

    protected virtual void Initialize(){}

    public BindInfo BindInfo;
}

[Serializable]
public class BindInfo
{
    [field: SerializeField]
    public BaseViewModel baseViewModel;
    [field: SerializeField]
    public string Field;
}
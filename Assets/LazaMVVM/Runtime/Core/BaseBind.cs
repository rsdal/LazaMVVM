using System.Reflection;
using UnityEngine;

public abstract class BaseBind : MonoBehaviour
{
    private void Start()
    {
        var listItem = BindInfo.viewModel.GetType().GetCustomAttribute<ListItem>();
        
        if (listItem != null)
        {
            Transform findTransform = transform;
            
            while (findTransform != null)
            {
                ViewModelProvider viewModelProvider = findTransform.GetComponent<ViewModelProvider>();
                
                if (viewModelProvider != null)
                {
                    BindInfo.viewModel = (Object)viewModelProvider.ViewModel;
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
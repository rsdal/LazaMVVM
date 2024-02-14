using System.Collections.Generic;
using TMPro;
using UnityEngine;

[BindFilter(typeof(List<>))]
public class ListBind : BaseFieldBind<List<BaseViewModel>>
{
    [SerializeField]
    private ViewModelProvider Template;
    [SerializeField]
    private Transform parent;
    
    // protected override void OnValueChanged(ViewModelListField<IViewModel> newValue)
    // {
    //     for (int i = 0; i < newValue.Count; i++)
    //     {
    //         var provider = GameObject.Instantiate(Template, parent);
    //         provider.ViewModel = newValue[i];
    //     }   
    // }
    

    protected override void OnValueChanged(List<BaseViewModel> newValue)
    {
        
    }
}
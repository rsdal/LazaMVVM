using LazaMVVM.Runtime.Attributes;
using LazaMVVM.Runtime.Core;
using UnityEngine;

[ListItem]
public class ViewModelItem : BaseViewModelItem
{
    public ViewModelItem()
    {
        
    }
    
    public ViewModelField<string> Name = new ViewModelField<string>();
    public ViewModelField<int> Index = new ViewModelField<int>();

    [ViewModelCommand]
    public void DebugIndex()
    {
        Debug.Log($"This is my index {Index.Value}");
    }
}
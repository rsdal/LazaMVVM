using LazaMVVM.Runtime.Attributes;
using LazaMVVM.Runtime.Core;
using UnityEngine;

[ListItem]
public class ViewModelItemBehaviour : BaseViewModelItem
{
    public ViewModelField<int> Index2 = new ViewModelField<int>();

    [ViewModelCommand]
    public void DebugIndex2()
    {
        Debug.Log($"This is my index {Index2.Value}");
    }
}
using UnityEngine;

[ListItem]
[CreateAssetMenu(fileName = "ViewModelItem", menuName = "ViewModels/New ViewModelItem")]
public class ViewModelItem : BaseViewModel
{
    public ViewModelField<int> Index = new ViewModelField<int>();

    [ViewModelCommand]
    public void DebugIndex()
    {
        Debug.Log($"This is my index {Index.Value}");
    }
}
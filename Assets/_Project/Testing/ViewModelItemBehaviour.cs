using UnityEngine;

[ListItem]
public class ViewModelItemBehaviour : MonoBehaviourViewModel
{
    public ViewModelField<int> Index = new ViewModelField<int>();

    [ViewModelCommand]
    public void DebugIndex()
    {
        Debug.Log($"This is my index {Index.Value}");
    }
}
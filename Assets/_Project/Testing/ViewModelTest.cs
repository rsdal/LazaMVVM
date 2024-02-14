using UnityEngine;

[CreateAssetMenu(fileName = "ViewModelTest", menuName = "ViewModels/New ViewModelTest")]
public class ViewModelTest : BaseViewModel
{
    public ViewModelField<int> PlayerName = new ViewModelField<int>();
    public ViewModelField<string> PlayerNameTest2 = new ViewModelField<string>();
    public ViewModeListField<ViewModelItem> viewmodelItens = new ViewModeListField<ViewModelItem>();
    
    public int x;
    
    [ViewModelCommand]
    public void Add()
    {
        PlayerName.Value = Random.Range(0, 100);

        for (int i = 0; i < 5; i++)
        {
            ViewModelItem viewModelItem = new ViewModelItem();
            viewModelItem.Index.Value = Random.Range(0, 1000);
            viewmodelItens.Add(viewModelItem);
        }
    }
    
    [ViewModelCommand]
    public void Remove()
    {
        viewmodelItens.RemoveAt(0);
    }
}
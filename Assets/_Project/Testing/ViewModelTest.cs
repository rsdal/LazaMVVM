using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ViewModelTest", menuName = "ViewModels/New ViewModelTest")]
public class ViewModelTest : ScriptableObjectViewModel
{
    //public ViewModelField<int> PlayerName = new ViewModelField<int>();
    //public ViewModelField<string> PlayerNameTest2 = new ViewModelField<string>();
    public ViewModelListField<ViewModelItem> viewmodelItens = new ViewModelListField<ViewModelItem>();
    // public ViewModelField<List<string>> Test = new ViewModelField<List<string>>();
    
    public int x;
    
    [ViewModelCommand]
    public void Add()
    {
        //PlayerName.Value = Random.Range(0, 100);

        ViewModelItem viewModelItem = ScriptableObject.CreateInstance<ViewModelItem>();
        viewModelItem.Index.Value = Random.Range(0, 1000);
        viewmodelItens.Add(viewModelItem);
        
        // Test.Value = new List<string>();
        
    }
    
    [ViewModelCommand]
    public void Remove()
    {
        viewmodelItens.RemoveAt(0);
    }
    
    [ViewModelCommand]
    public void DebugView()
    {
        Debug.Log("Debug!!");
    }
}
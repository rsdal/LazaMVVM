using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ViewModelTest", menuName = "ViewModels/New ViewModelTest")]
public class ViewModelTest : ScriptableObjectViewModel
{
    //public ViewModelField<int> PlayerName = new ViewModelField<int>();
    //public ViewModelField<string> PlayerNameTest2 = new ViewModelField<string>();
    public ViewModelListField<ViewModelItem> viewmodelItens = new ViewModelListField<ViewModelItem>();
    // public ViewModelField<List<string>> Test = new ViewModelField<List<string>>();

    public ViewModelListField<ViewModelItemBehaviour> viewModelItemBehaviours =
        new ViewModelListField<ViewModelItemBehaviour>();
    
    public int x;
    
    [ViewModelCommand]
    public void Add()
    {
        //PlayerName.Value = Random.Range(0, 100);

        ViewModelItem viewModelItem = ViewModelItem.CreateInstance<ViewModelItem>();
        viewModelItem.Index.Value = Random.Range(0, 1000);
        viewmodelItens.Add(viewModelItem);


        ViewModelItemBehaviour viewModelItemBehaviour = new GameObject("").AddComponent<ViewModelItemBehaviour>();
        viewModelItemBehaviour.Index.Value = Random.Range(0, 1000);
        viewModelItemBehaviours.Add(viewModelItemBehaviour);

        //TODO Add logic to create easier new itens
        
        
        // Test.Value = new List<string>();
        
    }
    
    [ViewModelCommand]
    public void Remove()
    {
        viewmodelItens.RemoveAt(0);
        viewModelItemBehaviours.RemoveAt(0);
    }
    
    [ViewModelCommand]
    public void DebugView()
    {
        Debug.Log("Debug!!");
    }
}
using System.Collections.Generic;
using LazaMVVM.Runtime.Attributes;
using LazaMVVM.Runtime.Core;
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

    public List<int> eita = new List<int>();
    
    public int x;
    
    [ViewModelCommand]
    public void Add()
    {
        //PlayerName.Value = Random.Range(0, 100);

        ViewModelItem viewModelItem = new ViewModelItem();
        viewModelItem.Index.Value = Random.Range(0, 1000);
        viewmodelItens.Add(viewModelItem);


        // ViewModelItemBehaviour viewModelItemBehaviour = ViewModelItemBehaviour.New<ViewModelItemBehaviour>();
        // viewModelItemBehaviour.Index.Value = Random.Range(0, 1000);
        // viewModelItemBehaviours.Add(viewModelItemBehaviour);

        // Test.Value = new List<string>();
        
    }
    
    [ViewModelCommand]
    public void Remove()
    {
        viewmodelItens.RemoveAt(0);
        //viewModelItemBehaviours.RemoveAt(0);
    }
    
    [ViewModelCommand]
    public void DebugView()
    {
        Debug.Log("Debug!!");
    }
}
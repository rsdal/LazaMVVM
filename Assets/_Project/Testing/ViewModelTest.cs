using UnityEngine;

[CreateAssetMenu(fileName = "ViewModelTest", menuName = "ViewModels/New ViewModelTest")]
public class ViewModelTest : BaseViewModel
{
    public ViewModelField<int> PlayerName = new ViewModelField<int>();
    public ViewModelField<string> PlayerNameTest2 = new ViewModelField<string>();

    public int x;
    
    [ViewModelCommand]
    public void TestCommand()
    {
        PlayerName.Value = Random.Range(0, 100);
    }
}
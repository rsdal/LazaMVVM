using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestViewModelMonobehaviour : MonoBehaviourViewModel
{
    public ViewModelField<string> testString = new ViewModelField<string>();

    private void Start()
    {
        testString.Value = "Eita!";
    }

    [ViewModelCommand]
    public void DestroyThisMF()
    {
        Debug.Log(testString.Value);
    }
}

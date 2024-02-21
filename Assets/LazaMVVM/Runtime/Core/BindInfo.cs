using System;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class BindInfo
{
    [field: SerializeField]
    public Object viewModel;
    [field: SerializeField]
    public string Field;
}
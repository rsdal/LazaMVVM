using System;
using UnityEngine;

public abstract class BaseBind : MonoBehaviour
{
    public BindInfo BindInfo;
}

[Serializable]
public class BindInfo
{
    [field: SerializeField]
    public BaseViewModel baseViewModel;
    [field: SerializeField]
    public string Field;
}
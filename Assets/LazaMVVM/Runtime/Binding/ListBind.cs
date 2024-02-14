using System.Collections.Generic;
using TMPro;
using UnityEngine;

[BindFilter(typeof(List<>))]
public class ListBind : BaseFieldBind<object>
{
    [SerializeField]
    private GameObject Template;
    [SerializeField]
    private Transform parent;

    protected override void OnValueChanged(object newValue)
    {
        
    }
}
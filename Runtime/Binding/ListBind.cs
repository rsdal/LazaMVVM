﻿using System.Collections.Generic;
using LazaMVVM.Runtime.Attributes;
using LazaMVVM.Runtime.Core;
using UnityEngine;

namespace LazaMVVM.Runtime.Binding
{
    [BindFilter(typeof(IList<>))]
    public class ListBind : BaseFieldBind<List<IViewModel>>
    {
        [SerializeField]
        private ViewModelProvider Template;
        [SerializeField]
        private Transform parent;

        private List<ViewModelProvider> instances = new List<ViewModelProvider>();
    
        protected override void OnValueChanged(List<IViewModel> newValue)
        {
            for (int i = instances.Count - 1; i >= 0; i--)
            {
                Destroy(instances[i].gameObject);   
                instances.RemoveAt(i);
            }
        
            for (int i = 0; i < newValue.Count; i++)
            {
                ViewModelProvider provider = Instantiate(Template, parent);   
            
                provider.ViewModel = newValue[i];
            
                instances.Add(provider);
            }   
        }
    }
}
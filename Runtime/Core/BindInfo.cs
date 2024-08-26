using System;
using LazaMVVM.Runtime.Attributes;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LazaMVVM.Runtime.Core
{
    [Serializable]
    public class BindInfo
    {
        [IViewModel(typeof(IViewModel))]
        [field: SerializeField]
        public Object viewModel;
        [field: SerializeField]
        public string Field;

        public IViewModel RuntimeValue;
    }
} 
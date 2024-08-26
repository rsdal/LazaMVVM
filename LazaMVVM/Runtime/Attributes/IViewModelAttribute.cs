using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LazaMVVM.Runtime.Attributes
{
    using UnityEngine;
    using System;

    [AttributeUsage(AttributeTargets.Field)]
    public class IViewModelAttribute : PropertyAttribute
    {
        public Type InterfaceType { get; private set; }

        public IViewModelAttribute(Type interfaceType)
        {
            InterfaceType = interfaceType;
        }
    }
}

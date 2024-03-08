using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace LazaMVVM.Runtime.Core
{
    public class MonoBehaviourViewModel : MonoBehaviour, IViewModel
    {
        private readonly ReflectionCache _reflectionCache = new ReflectionCache();
    
        public Dictionary<string, IViewModelField> GetFields()
        {
            return _reflectionCache.GetFields(this);
        }

        public bool GetFieldByName(string name, out IViewModelField field)
        {
            return _reflectionCache.GetFieldByName(this, name, out field);
        }

        public Dictionary<string, MethodInfo> GetMethods()
        {
            return _reflectionCache.GetMethods(this);
        }

        public bool GetMethodByName(string name, out MethodInfo method)
        {
            return _reflectionCache.GetMethodByName(this, name, out method);
        }
    }
}
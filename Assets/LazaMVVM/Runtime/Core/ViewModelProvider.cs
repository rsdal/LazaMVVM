using System;
using System.Collections.Generic;
using System.Reflection;
using LazaMVVM.Runtime.Attributes;
using UnityEngine;

namespace LazaMVVM.Runtime.Core
{
    [ListItem]
    public class ViewModelProvider : MonoBehaviour, IViewModel
    {
        public MyListBind myListBind;
        
        private readonly ReflectionCache _reflectionCache = new ReflectionCache();
        
        private IViewModel _savedViewModel;
        private string lastName;
        
        
        private IViewModel _viewModel { get; set; }
        public IViewModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }
                
                if (_savedViewModel != null && lastName == myListBind.ClassName)
                {
                    return _savedViewModel;
                }
                
                lastName = myListBind.ClassName;
                
                string[] parts = myListBind.ClassName.Split(',');

                if (parts.Length >= 2)
                {
                    Assembly assembly = Assembly.Load(parts[1].Trim());
            
                    Type type = assembly.GetType(parts[0].Trim());
            
                    if (type != null)
                    {
                        object instance = Activator.CreateInstance(type);

                        _savedViewModel = (IViewModel)instance;
                        
                        return _savedViewModel;
                    }
                    else
                    {
                        // Handle error if type is not found
                        throw new TypeLoadException($"Type '{parts[0].Trim()}' not found in assembly '{parts[1].Trim()}'.");
                    }
                }
                else
                {
                    // Handle error if assembly qualified name is invalid
                    throw new ArgumentException("Invalid assembly qualified name.", nameof(myListBind.ClassName));
                }
            }
            set => _viewModel = value;
        }
        
        public Dictionary<string, IViewModelField> GetFields()
        {
            return _reflectionCache.GetFields(ViewModel, true);
        }

        public bool GetFieldByName(string name, out IViewModelField field)
        {
            return _reflectionCache.GetFieldByName(ViewModel, name, out field);
        }

        public Dictionary<string, MethodInfo> GetMethods()
        {
            return _reflectionCache.GetMethods(ViewModel, true);
        }

        public bool GetMethodByName(string name, out MethodInfo method)
        {
            return _reflectionCache.GetMethodByName(ViewModel, name, out method);
        }
    }
}
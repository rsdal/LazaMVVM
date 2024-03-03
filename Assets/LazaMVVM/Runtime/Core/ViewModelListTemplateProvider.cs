using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System;
using LazaMVVM.Runtime.Attributes;

namespace LazaMVVM.Runtime.Core
{
    [ListItem]
    [CreateAssetMenu(menuName = "LazaMVVM/New View Model Template Provider", fileName = "ViewModelListTemplateProvider")]
    public class ViewModelListTemplateProvider : ScriptableObject, IViewModel
    {
        public MyListBind myListBind;
        private readonly ViewModel _viewModel = new ViewModel();
        
        private IViewModel _savedViewModel;
        private string lastName;
        
        private IViewModel Test
        {
            get
            {
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
        }
     
        public Dictionary<string, IViewModelField> GetFields()
        {
            return _viewModel.GetFields(Test, true);
        }

        public bool GetFieldByName(string name, out IViewModelField field)
        {
            return _viewModel.GetFieldByName(Test, name, out field);
        }

        public Dictionary<string, MethodInfo> GetMethods()
        {
            return _viewModel.GetMethods(Test, true);
        }

        public bool GetMethodByName(string name, out MethodInfo method)
        {
            return _viewModel.GetMethodByName(Test, name, out method);
        }
    }
}
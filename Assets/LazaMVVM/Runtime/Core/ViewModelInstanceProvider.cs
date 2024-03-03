using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System;
using LazaMVVM.Runtime.Attributes;
using Object = UnityEngine.Object;

namespace LazaMVVM.Runtime.Core
{
    [ListItem]
    [CreateAssetMenu(menuName = "LazaMVVM/New View Model Instance Provider", fileName = "ViewModelInstance")]
    public class ViewModelInstanceProvider : ScriptableObject, IViewModel
    {
        public MyListBind myListBind;
        private readonly ViewModel _viewModel = new ViewModel();
        
        private IViewModel _test;
        private string lastName;
        
        private IViewModel Test
        {
            get
            {
                if (_test != null && lastName == myListBind.ClassName)
                {
                    return _test;
                }
                
                lastName = myListBind.ClassName;
                
                string[] parts = myListBind.ClassName.Split(',');

                if (parts.Length >= 2)
                {
                    Assembly assembly = Assembly.Load(parts[1].Trim());
            
                    Type type = assembly.GetType(parts[0].Trim());
            
                    if (type != null)
                    {
                        //This is a really specific situation, since I don't need to keep a proper instance I'm using the activator
                        //This will be a null object because it's partially handled by Unity
                        //But it will work as a type and this will work for everything else
                        //Not pretty but it works for now.
                        var instance = Activator.CreateInstance(type);

                        _test = (IViewModel)instance;
                        
                        return _test;
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
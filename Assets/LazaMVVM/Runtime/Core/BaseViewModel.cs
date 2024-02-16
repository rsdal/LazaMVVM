using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class BaseViewModel : ScriptableObject
{
    private Dictionary<string, IViewModelField> fieldsDictionary { get; set; }
    private Dictionary<string, MethodInfo> methodsDictionary { get; set; }

    public Dictionary<string, IViewModelField> GetFields()
    {
        if (fieldsDictionary == null)
        {
            InitializeFields();
        }

        return fieldsDictionary;
    }
    
    private void InitializeFields()
    {
        fieldsDictionary = new Dictionary<string, IViewModelField>();
            
        Type type = this.GetType();
        FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        for (var index = 0; index < fields.Length; index++)
        {
            var field = fields[index];
            
            bool isIViewModelField = field.FieldType.GetInterfaces()
                .Any(i => i == typeof(IViewModelField));

            if (!isIViewModelField)
            {
                continue;
            }

            IViewModelField viewModelField = (IViewModelField)field.GetValue(this);
            fieldsDictionary.Add(field.Name, viewModelField);
        }
    }
    
    public IViewModelField GetFieldByName(string name)
    {
        Dictionary<string, IViewModelField> fields = GetFields();
        
        return fields[name];
    }

    public Dictionary<string, MethodInfo> GetMethods()
    {
        if (methodsDictionary == null)
        {
            InitializeMethods();
        }

        return methodsDictionary;
    }
    
    private void InitializeMethods()
    {
        methodsDictionary = new Dictionary<string, MethodInfo>();
        
        Type type = this.GetType();
        
        MethodInfo[] methods = type.GetMethods(
            BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (MethodInfo method in methods)
        {
            ViewModelCommand viewModelCommand = method.GetCustomAttribute<ViewModelCommand>();

            if (viewModelCommand != null)
            {
                methodsDictionary.Add(method.Name, method);
            }
        }
    }
    
    public MethodInfo GetMethodByName(string name)
    {
        Dictionary<string, MethodInfo> methods = GetMethods();
        return methods[name];
    }
}
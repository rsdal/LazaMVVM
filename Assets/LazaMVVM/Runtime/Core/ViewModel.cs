using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class ViewModel 
{
    private Dictionary<string, IViewModelField> fieldsDictionary { get; set; }
    private Dictionary<string, MethodInfo> methodsDictionary { get; set; }

    public Dictionary<string, IViewModelField> GetFields(IViewModel target)
    {
        if (fieldsDictionary == null)
        {
            InitializeFields(target);
        }

        return fieldsDictionary;
    }
    
    private void InitializeFields(IViewModel target)
    {
        fieldsDictionary = new Dictionary<string, IViewModelField>();
            
        Type type = target.GetType();
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

            IViewModelField viewModelField = (IViewModelField)field.GetValue(target);
            fieldsDictionary.Add(field.Name, viewModelField);
        }
    }
    
    public bool GetFieldByName(IViewModel target, string name, out IViewModelField field)
    {
        Dictionary<string, IViewModelField> fields = GetFields(target);

        return fieldsDictionary.TryGetValue(name, out field);
    }

    public Dictionary<string, MethodInfo> GetMethods(IViewModel target)
    {
        if (methodsDictionary == null)
        {
            InitializeMethods(target);
        }

        return methodsDictionary;
    }
    
    private void InitializeMethods(IViewModel target)
    {
        methodsDictionary = new Dictionary<string, MethodInfo>();
        
        Type type = target.GetType();
        
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
    
    public bool GetMethodByName(IViewModel target, string name, out MethodInfo method)
    {
        Dictionary<string, MethodInfo> methods = GetMethods(target);

        return methods.TryGetValue(name, out method);
    }
}
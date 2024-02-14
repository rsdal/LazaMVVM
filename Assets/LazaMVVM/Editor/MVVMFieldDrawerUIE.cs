using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(BindInfo))]
public class MVVMFieldDrawerUIE : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        var container = new VisualElement();

        SerializedProperty viewModelSerializedProperty = property.FindPropertyRelative("baseViewModel");
        var viewModelField =  new PropertyField(viewModelSerializedProperty, "ViewModel");
        container.Add(viewModelField);
        
        if (viewModelSerializedProperty.boxedValue != null)
        {
            List<string> possibleFields = new List<string>();

            object filter = property.serializedObject.targetObject.GetType()
                .GetCustomAttributes(false)
                .FirstOrDefault();

            if (filter is CommandBindFilter)
            {
               Dictionary<string, MethodInfo> methods = ((BaseViewModel)viewModelSerializedProperty.boxedValue).GetMethods();

               foreach (KeyValuePair<string, MethodInfo> method in methods)
               {
                   possibleFields.Add(method.Key);
               }
            }
            else
            {
                BindFilter bindFilter = filter as BindFilter;

                Type[] filters = bindFilter.GetFiltersTypes;
         
                Dictionary<string, IViewModelField> fields = ((BaseViewModel)viewModelSerializedProperty.boxedValue).GetFields();
            
                foreach (KeyValuePair<string, IViewModelField> currentField in fields)
                {
                    Type x = currentField.Value.GetObject.GetType();
                    if (filters.Contains(x))
                    {
                        possibleFields.Add(currentField.Key);
                    }
                } 
            }
            
            DropdownField nameField = new DropdownField("Fields",
                    possibleFields,
                0);

            SerializedProperty field = property.FindPropertyRelative("Field");
            nameField.BindProperty(field);
            
            container.Add(nameField);
        }
        
        return container;
    }
}

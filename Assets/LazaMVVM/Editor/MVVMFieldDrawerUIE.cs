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
    private List<string> _possibleFields = new List<string>();
    
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        var container = new VisualElement();

        SerializedProperty viewModelSerializedProperty = property.FindPropertyRelative("viewModel");
        var viewModelField = new PropertyField(viewModelSerializedProperty, "ViewModel");
        container.Add(viewModelField);

        DropdownField dropDownField = new DropdownField("Fields",
            _possibleFields,
            0);

        SerializedProperty dropDownSerializedField = property.FindPropertyRelative("Field");
        dropDownField.BindProperty(dropDownSerializedField);
        container.Add(dropDownField);
        
        viewModelField.RegisterValueChangeCallback(evt =>
        {
            dropDownSerializedField.serializedObject.ApplyModifiedProperties();
            Refresh(property, viewModelSerializedProperty);
        });
        
        return container;
    }

    private void Refresh(SerializedProperty property, SerializedProperty viewModelSerializedProperty)
    {
        if (viewModelSerializedProperty.boxedValue != null)
        {
            _possibleFields.Clear();

            object filter = property.serializedObject.targetObject.GetType()
                .GetCustomAttributes(false)
                .FirstOrDefault();

            if (filter is CommandBindFilter)
            {
                Dictionary<string, MethodInfo> methods =
                    ((IViewModel)viewModelSerializedProperty.boxedValue).GetMethods();

                foreach (KeyValuePair<string, MethodInfo> method in methods)
                {
                    _possibleFields.Add(method.Key);
                }
            }
            else
            {
                BindFilter bindFilter = filter as BindFilter;

                Type[] filters = bindFilter.GetFiltersTypes;

                Dictionary<string, IViewModelField> fields =
                    ((IViewModel)viewModelSerializedProperty.boxedValue).GetFields();

                foreach (KeyValuePair<string, IViewModelField> currentField in fields)
                {
                    Type fieldType = currentField.Value.GetObject.GetType();

                    //Is it a list?
                    if (fieldType.IsGenericType)
                    {
                        fieldType = fieldType.GetGenericTypeDefinition();
                    }

                    if (filters.Contains(fieldType))
                    {
                        _possibleFields.Add(currentField.Key);
                    }
                }
            }
        }
    }
}
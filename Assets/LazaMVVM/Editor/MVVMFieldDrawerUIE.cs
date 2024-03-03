using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LazaMVVM.Runtime.Attributes;
using LazaMVVM.Runtime.Core;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace LazaMVVM.Editor
{
    [CustomPropertyDrawer(typeof(BindInfo))]
    public class MVVMFieldDrawerUIE : PropertyDrawer
    {
        private List<string> _possibleFields = new List<string>();
        private VisualElement _container;
        private Label _warningLabel;
        private DropdownField _dropDownField;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            _container = new VisualElement();
            
            SerializedProperty viewModelSerializedProperty = property.FindPropertyRelative("viewModel");
            var viewModelField = new PropertyField(viewModelSerializedProperty, "ViewModel");
            _container.Add(viewModelField);

            _dropDownField = new DropdownField("Fields",
                _possibleFields,
                0);

            SerializedProperty dropDownSerializedField = property.FindPropertyRelative("Field");
            _dropDownField.BindProperty(dropDownSerializedField);
            _container.Add(_dropDownField);

            _warningLabel = new Label();
            _warningLabel.style.color = Color.red;
            _container.Add(_warningLabel);
            
            viewModelField.RegisterValueChangeCallback(evt =>
            {
                dropDownSerializedField.serializedObject.ApplyModifiedProperties();
                Refresh(property, viewModelSerializedProperty);
            });

            return _container;
        }

        private void Refresh(SerializedProperty property, SerializedProperty viewModelSerializedProperty)
        {
            if (viewModelSerializedProperty.boxedValue == null || viewModelSerializedProperty.boxedValue is not IViewModel)
            {
                _dropDownField.SetEnabled(false);
                _warningLabel.text = $"Field {property.displayName} does not implement {typeof(IViewModel)}";
                return;
            }

            _dropDownField.SetEnabled(true);
            _warningLabel.text = "";

            _possibleFields.Clear();

            object filter = property.serializedObject.targetObject.GetType()
                .GetCustomAttributes(false)
                .FirstOrDefault();

            if (IsCommand(filter))
            {
                HandleCommand(viewModelSerializedProperty);

                return;
            }

            HandleField(viewModelSerializedProperty, filter);
        }
        
        private void HandleCommand(SerializedProperty viewModelSerializedProperty)
        {
            Dictionary<string, MethodInfo> methods =
                ((IViewModel)viewModelSerializedProperty.boxedValue).GetMethods();

            foreach (KeyValuePair<string, MethodInfo> method in methods)
            {
                _possibleFields.Add(method.Key);
            }
        }

        private static bool IsCommand(object filter)
        {
            return filter is CommandBindFilter;
        }

        private void HandleField(SerializedProperty viewModelSerializedProperty, object filter)
        {
            BindFilter bindFilter = filter as BindFilter;

            Type[] filters = bindFilter.GetFiltersTypes;

            Dictionary<string, IViewModelField> fields =
                ((IViewModel)viewModelSerializedProperty.boxedValue).GetFields();

            foreach (KeyValuePair<string, IViewModelField> currentField in fields) 
            {
                Type fieldType = currentField.Value.GetType();

                bool directlyImplementsIListT = fieldType.GetInterface(typeof(IList<>).Name) != null;

                if (directlyImplementsIListT) 
                {
                    _possibleFields.Add(currentField.Key);
                }
                else if (fieldType.IsGenericType)
                {
                    fieldType = fieldType.GetGenericArguments().First();
                }

                if (filters.Contains(fieldType))
                {
                    _possibleFields.Add(currentField.Key);
                }
            }
        }
    }
}
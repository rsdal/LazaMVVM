using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;
using System.Reflection;
using LazaMVVM.Runtime;
using LazaMVVM.Runtime.Attributes;

[CustomPropertyDrawer(typeof(EnumSelector))]
public class EnumSelectorDrawer : PropertyDrawer
{
    private static Type[] enumTypes; 
    private static string[] enumTypeNames;
    private static string[] prioritizedEnumTypeNames;

    static EnumSelectorDrawer()
    {
        enumTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsEnum && type.GetCustomAttribute<MVVMEnum>() != null)
            .ToArray();
        
        enumTypeNames = enumTypes.Select(t => t.AssemblyQualifiedName).ToArray();
    }

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        var container = new VisualElement();

        var enumTypeField = new PopupField<string>("Enum Type", enumTypeNames.ToList(), 0);
        container.Add(enumTypeField);

        var enumValueField = new PopupField<string>("Enum Value", new List<string>(), 0);
        container.Add(enumValueField);

        SerializedProperty enumTypeProp = property.FindPropertyRelative("selectedEnumType");
        SerializedProperty enumValueProp = property.FindPropertyRelative("selectedEnumValue");

        enumTypeField.RegisterValueChangedCallback(evt =>
        {
            string selectedTypeName = evt.newValue;
            Type selectedType = Type.GetType(selectedTypeName);
            enumTypeProp.stringValue = selectedType.AssemblyQualifiedName;

            var enumValues = Enum.GetNames(selectedType);
            enumValueField.choices = new List<string>(enumValues);
            enumValueField.index = 0; 
            enumValueProp.stringValue = enumValues.FirstOrDefault();

            property.serializedObject.ApplyModifiedProperties();
        });

        enumValueField.RegisterValueChangedCallback(evt =>
        {
            string selectedValue = evt.newValue;
            enumValueProp.stringValue = selectedValue;

            property.serializedObject.ApplyModifiedProperties();
        });

        Type currentType = Type.GetType(enumTypeProp.stringValue);
        if (currentType != null && currentType.IsEnum)
        {
            enumTypeField.value = currentType.FullName;
            var enumValues = Enum.GetNames(currentType);
            enumValueField.choices = new List<string>(enumValues);
            enumValueField.index = Array.IndexOf(enumValues, enumValueProp.stringValue);
        }

        return container;
    }
}

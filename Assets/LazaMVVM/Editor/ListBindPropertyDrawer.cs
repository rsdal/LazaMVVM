using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LazaMVVM.Runtime.Attributes;
using LazaMVVM.Runtime.Core;
using UnityEditor.UIElements;

[CustomPropertyDrawer(typeof(MyListBind))]
public class ListBindPropertyDrawer : PropertyDrawer
{
    private List<Type> availableTypes = new List<Type>(); // List to store available types
    private int selectedIndex = 0; // Index of selected type

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        // Create a new VisualElement
        VisualElement root = new VisualElement();

        // Find all types in the assembly
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .ToList();

        // Filter types to find those that contain ListBind field
        availableTypes = types.Where(type =>
            type.GetCustomAttribute<ListItem>() != null && type != typeof(ViewModelInstanceProvider)
        ).ToList();

        // Convert available types to string array
        string[] typeNames = availableTypes.Select(type => type.AssemblyQualifiedName).ToArray();
        
        // Create a PopupField<string> to select the type
        PopupField<string> popupField = new PopupField<string>("Select Type", typeNames.ToList(), selectedIndex);
        
        SerializedProperty dropDownSerializedField = property.FindPropertyRelative("ClassName");
        popupField.BindProperty(dropDownSerializedField);
        
        // Add the PopupField to the VisualElement
        root.Add(popupField);

        return root;
    }
}
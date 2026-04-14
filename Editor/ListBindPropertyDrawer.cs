 using UnityEditor;
using UnityEngine.UIElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LazaMVVM.Runtime.Attributes;
using LazaMVVM.Runtime.Core;
using UnityEditor.Compilation;
using UnityEditor.UIElements;

[CustomPropertyDrawer(typeof(MyListBind))]
public class ListBindPropertyDrawer : PropertyDrawer
{
    // Static cache to avoid repeated reflection calls
    private static List<Type> cachedAvailableTypes;
    private static string[] cachedTypeNames;
    private static bool isInitialized = false;
    private static bool eventsSubscribed = false;
    
    private int selectedIndex = 0; // Index of selected type

    // Subscribe to events that should trigger cache refresh
    private static void SubscribeToEvents()
    {
        if (eventsSubscribed) return;
        
        // Listen for assembly loading events
        AppDomain.CurrentDomain.AssemblyLoad += OnAssemblyLoaded;
        
        // Listen for Unity domain reload events
        AssemblyReloadEvents.beforeAssemblyReload += OnBeforeAssemblyReload;
        AssemblyReloadEvents.afterAssemblyReload += OnAfterAssemblyReload;
        
        // Listen for script compilation events
        CompilationPipeline.compilationStarted += OnCompilationStarted;
        CompilationPipeline.compilationFinished += OnCompilationFinished;
        
        eventsSubscribed = true;
    } 
    
    private static void OnAssemblyLoaded(object sender, AssemblyLoadEventArgs args)
    {
        // Only invalidate if the loaded assembly might contain relevant types
        try
        {
            var hasRelevantTypes = args.LoadedAssembly.GetTypes()
                .Any(type => type.GetCustomAttribute<ListItem>() != null);
            
            if (hasRelevantTypes)
            {
                InvalidateCache();
            }
        }
        catch
        {
            // If we can't check the assembly, invalidate cache to be safe
            InvalidateCache();
        }
    }
    
    private static void OnBeforeAssemblyReload()
    {
        // Clear cache before assembly reload
        InvalidateCache();
    }
    
    private static void OnAfterAssemblyReload()
    {
        // Cache will be rebuilt on next access
        // Re-subscribe to events since they're cleared on domain reload
        eventsSubscribed = false;
    }
    
    private static void OnCompilationStarted(object obj)
    {
        // Invalidate cache when compilation starts
        InvalidateCache();
    }
    
    private static void OnCompilationFinished(object obj)
    {
        // Cache will be rebuilt on next access
    }
    
    private static void InvalidateCache()
    {
        isInitialized = false;
        cachedAvailableTypes = null;
        cachedTypeNames = null;
    }

    // Initialize cache once
    private static void InitializeTypeCache()
    {
        if (isInitialized) return;
        
        // Subscribe to events on first initialization
        SubscribeToEvents();
        
        // Find all types in the assembly - cache this expensive operation
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .Where(assembly => !assembly.IsDynamic) // Skip dynamic assemblies
            .SelectMany(assembly => {
                try 
                {
                    return assembly.GetTypes();
                }
                catch (ReflectionTypeLoadException ex)
                {
                    // Handle assemblies that might have loading issues
                    return ex.Types.Where(t => t != null);
                }
                catch
                {
                    return Enumerable.Empty<Type>();
                }
            })
            .ToList();

        // Filter types to find those that contain ListBind field
        cachedAvailableTypes = types.Where(type =>
            type != null &&
            type.GetCustomAttribute<ListItem>() != null && 
            type != typeof(ViewModelProvider)
        ).ToList();

        // Convert available types to string array
        cachedTypeNames = cachedAvailableTypes.Select(type => type.AssemblyQualifiedName).ToArray();
        
        isInitialized = true;
    }

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        // Initialize cache if needed
        InitializeTypeCache();
        
        // Create a new VisualElement
        VisualElement root = new VisualElement();
        
        // Create a PopupField<string> to select the type using cached data
        PopupField<string> popupField = new PopupField<string>("Select Type", cachedTypeNames.ToList(), selectedIndex);
        
        SerializedProperty dropDownSerializedField = property.FindPropertyRelative("ClassName");
        popupField.BindProperty(dropDownSerializedField);
        
        // Add the PopupField to the VisualElement
        root.Add(popupField);

        return root;
    }
}
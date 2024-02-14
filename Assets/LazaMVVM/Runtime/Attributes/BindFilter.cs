using System;
using System.Linq;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class BindFilter : Attribute
{
    public Type[] GetFiltersTypes => filterTypes;
    private Type[] filterTypes;
    
    public BindFilter(params Type[] types)
    {
        filterTypes = types?.ToArray();
    }
}
﻿using System.Collections.Generic;
using System.Reflection;

namespace LazaMVVM.Runtime.Core
{
    public interface IViewModel
    {
        Dictionary<string, IViewModelField> GetFields();
        bool GetFieldByName(string name, out IViewModelField field);
        Dictionary<string, MethodInfo> GetMethods();
        bool GetMethodByName(string name, out MethodInfo method);
    }
}
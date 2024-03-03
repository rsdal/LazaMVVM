﻿using System.Collections.Generic;
using System.Reflection;
using LazaMVVM.Runtime.Core;
using UnityEngine;

namespace LazaMVVM.Runtime.Core
{
    public abstract class BaseViewModelItem : Object, IViewModel
    {
        private readonly ViewModel _viewModel = new ViewModel();

        protected BaseViewModelItem()
        {
            
        }
        
        public Dictionary<string, IViewModelField> GetFields()
        {
            return _viewModel.GetFields(this);
        }

        public bool GetFieldByName(string name, out IViewModelField field)
        {
            return _viewModel.GetFieldByName(this, name, out field);
        }

        public Dictionary<string, MethodInfo> GetMethods()
        {
            return _viewModel.GetMethods(this);
        }

        public bool GetMethodByName(string name, out MethodInfo method)
        {
            return _viewModel.GetMethodByName(this, name, out method);
        }
    }
}
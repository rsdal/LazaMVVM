using System.Reflection;
using LazaMVVM.Runtime.Attributes;
using UnityEngine;

namespace LazaMVVM.Runtime.Core
{
    public abstract class BaseBind : MonoBehaviour
    {
        private void Start()
        {
            if (IsThisViewModelAListItem())
            {
                FindViewModelProvider();
            }
            else
            {
                UpdateRuntimeViewModel();
            }
            
            Initialize();
        }

        private bool IsThisViewModelAListItem()
        {
            var listItem = BindInfo.viewModel.GetType().GetCustomAttribute<ListItem>();
            
            return listItem != null;
        }
        
        private void FindViewModelProvider()
        {
            BindInfo.RuntimeValue = ((ViewModelProvider)BindInfo.viewModel).ViewModel;
        }

        private void UpdateRuntimeViewModel()
        {
            IViewModel viewModel = BindInfo.viewModel as IViewModel;
            BindInfo.RuntimeValue = viewModel;
        }
        
        protected virtual void Initialize(){}

        public BindInfo BindInfo;
    }
}
using System.Reflection;
using LazaMVVM.Runtime.Attributes;
using UnityEngine;

namespace LazaMVVM.Runtime.Core
{
    public abstract class BaseBind : MonoBehaviour
    {
        private void Start()
        {
            var listItem = BindInfo.viewModel.GetType().GetCustomAttribute<ListItem>();
        
            if (listItem != null)
            {
                Transform findTransform = transform;
            
                while (findTransform != null)
                {
                    ViewModelProvider viewModelProvider = findTransform.GetComponent<ViewModelProvider>();
                
                    if (viewModelProvider != null)
                    {
                        BindInfo.viewModel = (Object)viewModelProvider.ViewModel;
                        break;
                    }

                    if (transform.parent == transform.root)
                    {
                        break;
                    }
                
                    findTransform = transform.parent;
                }
            }

            Initialize();
        }

        protected virtual void Initialize(){}

        public BindInfo BindInfo;
    }
}
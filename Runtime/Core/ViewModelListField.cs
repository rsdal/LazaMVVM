using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LazaMVVM.Runtime.Core
{
    public class ViewModelListField<T> : IViewModelField, IList<T> where T : IViewModel
    {
        [SerializeField] 
        private List<IViewModel> propertyValue = new();

        public Action<object> OnValueChanged { get; set; }
        public object GetObject => propertyValue;

        public void SetObject(object value)
        {
            propertyValue = (List<IViewModel>)value;
        }
    
        public IEnumerator<T> GetEnumerator()
        {
            foreach (IViewModel item in propertyValue)
            {
                yield return (T)item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            propertyValue.Add(item);
            OnValueChanged?.Invoke(propertyValue);
        }

        public void Clear()
        {
            propertyValue.Clear();
            OnValueChanged?.Invoke(propertyValue);
        }

        public bool Contains(T item)
        {
            return propertyValue.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            propertyValue.ToArray().CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            bool isRemoved = propertyValue.Remove(item);

            OnValueChanged?.Invoke(propertyValue);
        
            return isRemoved;
        }

        public int Count => propertyValue.Count;
        public bool IsReadOnly => false;
        public int IndexOf(T item)
        {
            return propertyValue.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            propertyValue.Insert(index, item);
            OnValueChanged?.Invoke(propertyValue);
        }

        public void RemoveAt(int index)
        {
            propertyValue.RemoveAt(index);
            OnValueChanged?.Invoke(propertyValue);
        }

        public T this[int index]
        {
            get => (T)propertyValue[index];
            set
            {
                propertyValue[index] = value;
                OnValueChanged?.Invoke(propertyValue);
            }
        }
    }
}

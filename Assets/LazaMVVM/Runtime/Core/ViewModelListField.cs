using System.Collections;
using System.Collections.Generic;

public class ViewModelListField<T> : ViewModelField<List<BaseViewModel>>, IList<T> where T : BaseViewModel
{
    public ViewModelListField()
    {
        Value = new List<BaseViewModel>();
    }
    
    public new IReadOnlyList<BaseViewModel> Value
    {
        get => base.Value;

        set => base.Value = value != null ? new List<BaseViewModel>(value) : new List<BaseViewModel>();
    }
    
    public IEnumerator<T> GetEnumerator()
    {
        return ((IEnumerable<T>) Value).GetEnumerator();
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
        propertyValue.CopyTo(array, arrayIndex);
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
        get => (T)Value[index];
        set
        {
            propertyValue[index] = value;
            OnValueChanged?.Invoke(propertyValue);
        }
    }
}
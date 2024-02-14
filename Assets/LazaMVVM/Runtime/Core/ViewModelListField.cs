using System;
using System.Collections;
using System.Collections.Generic;

public class ViewModelListField<T> : IViewModelField, IList<T> where T : BaseViewModel
{
    public Action<object> OnValueChanged { get; set; }
    public object GetObject => _baseViewModels;

    private readonly IList<T> _baseViewModels = new List<T>();

    public IEnumerator<T> GetEnumerator()
    {
        return _baseViewModels.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(T item)
    {
        _baseViewModels.Add(item);
        OnValueChanged?.Invoke(_baseViewModels);
    }

    public void Clear()
    {
        _baseViewModels.Clear();
    }

    public bool Contains(T item)
    {
        return _baseViewModels.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        _baseViewModels.CopyTo(array, arrayIndex);
    }

    public bool Remove(T item)
    {
        bool isRemoved = _baseViewModels.Remove(item);
        OnValueChanged?.Invoke(_baseViewModels);
        return isRemoved;
    }

    public int Count => _baseViewModels.Count;
    public bool IsReadOnly => _baseViewModels.IsReadOnly;
    public int IndexOf(T item)
    {
        return _baseViewModels.IndexOf(item);
    }

    public void Insert(int index, T item)
    {
        _baseViewModels.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
        _baseViewModels.RemoveAt(index);
    }

    public T this[int index]
    {
        get => _baseViewModels[index];
        set => _baseViewModels[index] = value;
    }
}
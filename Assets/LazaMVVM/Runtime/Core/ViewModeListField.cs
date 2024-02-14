using System;
using System.Collections;
using System.Collections.Generic;

public class ViewModeListField<T> : IViewModelField, IList<BaseViewModel> where T : BaseViewModel
{
    public Action<object> OnValueChanged { get; set; }
    public object GetObject => this;

    private readonly IList<BaseViewModel> _baseViewModels = new List<BaseViewModel>();

    public IEnumerator<BaseViewModel> GetEnumerator()
    {
        return _baseViewModels.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(BaseViewModel item)
    {
        _baseViewModels.Add(item);
        OnValueChanged?.Invoke(_baseViewModels);
    }

    public void Clear()
    {
        _baseViewModels.Clear();
    }

    public bool Contains(BaseViewModel item)
    {
        return _baseViewModels.Contains(item);
    }

    public void CopyTo(BaseViewModel[] array, int arrayIndex)
    {
        _baseViewModels.CopyTo(array, arrayIndex);
    }

    public bool Remove(BaseViewModel item)
    {
        bool isRemoved = _baseViewModels.Remove(item);
        OnValueChanged?.Invoke(_baseViewModels);
        return isRemoved;
    }

    public int Count => _baseViewModels.Count;
    public bool IsReadOnly => _baseViewModels.IsReadOnly;
    public int IndexOf(BaseViewModel item)
    {
        return _baseViewModels.IndexOf(item);
    }

    public void Insert(int index, BaseViewModel item)
    {
        _baseViewModels.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
        _baseViewModels.RemoveAt(index);
    }

    public BaseViewModel this[int index]
    {
        get => _baseViewModels[index];
        set => _baseViewModels[index] = value;
    }
}
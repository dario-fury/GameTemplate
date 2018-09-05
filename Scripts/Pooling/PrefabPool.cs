﻿using System.Collections.Generic;
using UnityEngine;

public class PrefabPool<T> where T : Component
{
    private readonly T _prefab;
    private readonly Stack<T> _pool;
    
    public PrefabPool(T prefab)
    {
        _prefab = prefab;
        _pool = new Stack<T>();
    }
    
    ~PrefabPool()
    {
        Clear();
    }

    public T Pop()
    {
        var obj = _pool.Count == 0 ? Object.Instantiate(_prefab) : _pool.Pop();
        obj.transform.SetParent(null);
        obj.gameObject.SetActive(true);
        obj.gameObject.hideFlags = HideFlags.None;
        return obj;
    }

    public void Push(T obj)
    {
        obj.transform.SetParent(null);
        obj.gameObject.SetActive(false);
        obj.gameObject.hideFlags = HideFlags.HideInHierarchy;
        _pool.Push(obj);
    }

    public void Clear()
    {
        while (_pool.Count > 0)
            Object.Destroy(_pool.Pop().gameObject);
    }
}
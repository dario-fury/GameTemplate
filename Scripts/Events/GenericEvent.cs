﻿using System;
using UnityEngine;

namespace GameTemplate
{
    public class GenericEvent<T> : ScriptableObject
    {
        // ReSharper disable once InconsistentNaming
        [SerializeField, HideInInspector] protected T _defaultValue;
        public T DefaultValue => _defaultValue;
    
        private event Action<T> Action;

        public void Subscribe(Action<T> a)
        {
            Action += a;
        }

        public void Unsubscribe(Action<T> a)
        {
            Action -= a;
        }

        public void Invoke(T arg)
        {
            Action?.Invoke(arg);
        }

        public void InvokeWithDefault()
        {
            Invoke(_defaultValue);
        }
    }
}

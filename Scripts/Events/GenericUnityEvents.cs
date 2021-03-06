﻿using System;
using UnityEngine;
using UnityEngine.Events;

namespace GameTemplate
{
    [Serializable]
    public class UnityEventBool : UnityEvent<bool> {}

    [Serializable]
    public class UnityEventFloat : UnityEvent<float> {}

    [Serializable]
    public class UnityEventInt : UnityEvent<int> {}

    [Serializable]
    public class UnityEventString : UnityEvent<string> {}
    
    [Serializable]
    public class UnityEventGameObject : UnityEvent<GameObject> {}
}
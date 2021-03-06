﻿using GameTemplate.Promises;

namespace GameTemplate
{
    public enum EAnimateOnOffState
    {
        On,
        Off,
        AnimatingOn,
        AnimatingOff
    }

    public interface IAnimateOnOff
    {
        IPromise AnimateOn(bool unscaled = false);

        IPromise AnimateOff(bool unscaled = false);

        void SetOn();

        void SetOff();

        float OnDuration { get; }
        float OffDuration { get; }
    
        AnimateOnOffGroup Group { get; set; }
    
        bool IgnoreAnimationGroup { get; }

        EAnimateOnOffState CurrentState { get; }
    }
}
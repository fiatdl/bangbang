using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDisplayHP
{
    public event EventHandler<OnDamegeTakedEventArgs> OnDamegeTaked;

    public class OnDamegeTakedEventArgs : EventArgs

    {
        public float hpRemainNormalized;
        public float shield;
    }
}

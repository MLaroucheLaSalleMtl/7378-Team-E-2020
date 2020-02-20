using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostureSystem
{
    public event EventHandler OnPostureChanged;
    public event EventHandler OnPostureBroken;

    private int postureAmount;
    private int postureAmountMax;

    public PostureSystem()
    {
        postureAmountMax = 100;
        postureAmount = 0;
    }

    public float GetPostureNormalized()
        => (float)postureAmount / postureAmountMax;

    public void PostureIncrease(int amount)
    {
        postureAmount += amount;
        if (OnPostureChanged != null) OnPostureChanged(this, EventArgs.Empty);

        if (postureAmount >= postureAmountMax)
        {
            // Posture broken
            postureAmount = postureAmountMax;
            if (OnPostureBroken != null) OnPostureBroken(this, EventArgs.Empty);
        }
    }

    public void PostureDecrease(int amount)
    {
        postureAmount -= amount;
        if (postureAmount <= 0) postureAmount = 0;
        if (OnPostureChanged != null) OnPostureChanged(this, EventArgs.Empty);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{
    private int hpAmount;
    private int hpAmountMax;
    
    public event EventHandler OnDead;
    public event EventHandler OnHpChanged;

    public HealthSystem()
    {
        hpAmount = hpAmountMax;
    }

    public int GetHp()
        => hpAmountMax;

    public int GetCurrentHp
        => hpAmount;

    public void SetHp(int amount)
        => hpAmountMax = amount;

    public float GetHpNormalize()
        => (float)hpAmount / hpAmountMax;

    public void HealHp(int healAmount)
    {
        hpAmount += healAmount;
        if (hpAmount > hpAmountMax) hpAmount = hpAmountMax;
        if (OnHpChanged != null) OnHpChanged(this, EventArgs.Empty);
    }

    public void HpDamage(int damageAmount)
    {
        hpAmount -= damageAmount;

        if (OnHpChanged != null)
            OnHpChanged(this, EventArgs.Empty);

        if(hpAmount <= 0)
        {
            hpAmount = 0;
            if (OnDead != null)
                OnDead(this, EventArgs.Empty);
        }
    }
}

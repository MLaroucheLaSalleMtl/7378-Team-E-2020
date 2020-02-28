using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICombat : CharacterCombat
{
    public override void Attack(CharacterStat targetStats)
    {
        base.Attack(targetStats);
    }
    public override void DoDamage(CharacterStat stat)
    {
        base.DoDamage(stat);
    }
}

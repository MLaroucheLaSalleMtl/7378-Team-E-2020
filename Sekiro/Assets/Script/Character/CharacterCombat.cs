using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterStat))]
public class CharacterCombat : MonoBehaviour
{
    CharacterStat myStat;

    void Start()
    {
        myStat = GetComponent<CharacterStat>();
    }

    public virtual void Attack(CharacterStat targetStats)
    {
        if (targetStats.alive)
            DoDamage(targetStats);
    }

    //public void Defend()
    //{
    //    int value = Random.Range(0, 11);
    //    animator.PerformDefend();
    //    DoDamage(myStats);
    //}

    public virtual void DoDamage(CharacterStat stat)
        => stat.TakeDamage(myStat.damage.GetValue());
}

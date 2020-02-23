using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : CharacterStat
{
    IEnumerator WaitToBeDisable()
    {
        yield return new WaitForSeconds(5);

        gameObject.SetActive(false);
    }


    public override void Die()
    {
        base.Die();
        StartCoroutine(WaitToBeDisable());
    }
}

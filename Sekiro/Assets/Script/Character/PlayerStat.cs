using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : CharacterStat
{
    IEnumerator WaitToBeDisable()
    {
        yield return new WaitForSeconds(5);

        gameObject.SetActive(false);
    }

    public override void SetHealthBar(Slider slider)
    {
        base.SetHealthBar(slider);
    }


    public override void Die()
    {
        base.Die();
        StartCoroutine(WaitToBeDisable());
    }
}

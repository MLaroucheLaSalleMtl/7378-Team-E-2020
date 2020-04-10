using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStat : CharacterStat
{
    IEnumerator WaitToBeDisable()
    {
        SpawnItem spawn = gameObject.GetComponent<SpawnItem>();
        if (spawn != null)
        {
            spawn.DropItem();
        }
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
        alive = false;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        controller.ExcuteTriggerAnimation("Die");
        StartCoroutine(WaitToBeDisable());
    }
}

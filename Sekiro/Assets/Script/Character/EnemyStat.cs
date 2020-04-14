using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStat : CharacterStat
{
    [SerializeField] private GameObject executionArea = null;
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

    public void ExecutionDie1()
    {
        base.ExecutionDie();
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        executionArea.SetActive(false);
        controller.AnimExeDie1();
    }

    public void ExecutionDie2()
    {
        base.ExecutionDie();
        controller.AnimExeDie2();
        StartCoroutine(WaitToBeDisable());
    }
}

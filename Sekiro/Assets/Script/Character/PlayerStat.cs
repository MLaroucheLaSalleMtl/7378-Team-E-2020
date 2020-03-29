using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerStat : CharacterStat //Player Revive and Die handle
{
    private int playerLife = 2;
    [SerializeField] private GameObject xStroke1 = null;
    [SerializeField] private GameObject xStroke2 = null;
    [SerializeField] private float delayRevive = 5f;
    [SerializeField] private GameObject reviveHealthBar = null;
    private bool isReviving = false;

   
   

    public override void SetHealthBar(Slider slider)
    {
        base.SetHealthBar(slider);
    }

    public override void Die()
    {
        PlayerDie();
    }

    private void PlayerDie()
    {
        playerLife--;
        if (playerLife >= 1)
        {
            if (xStroke1 != null)
            {
                xStroke1.SetActive(true);
            }
            StartCoroutine("Revive");
        }
        else if(playerLife < 1 && !isReviving)
        {
            if (xStroke2 != null)
            {
                xStroke2.SetActive(true);
            }
            gameObject.GetComponent<CharacterAnimation>().AnimationDie();
            alive = false;
        }
    }

    IEnumerator Revive()
    {
        gameObject.GetComponent<CharacterAnimation>().AnimationFaint();
        DisableOnDie();
        Invoke("Refill", 3f);
        yield return new WaitForSeconds(delayRevive);
        reviveHealthBar.SetActive(false);
        SetHp();
        RefillHealthBar();
    }

    public void Heal()
    {
        SetHp();
        RefillHealthBar();
    }

    public bool CanHeal()
    {
        if(currentHealth < GetMaxHealth())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Refill()
    {
        reviveHealthBar.SetActive(true);
    }

    private void DisableOnDie()
    {
        isReviving = true;
        alive = false;
        gameObject.GetComponent<PlayerController>().enabled = false;
    }

    private void EnableOnRevive()
    {
        alive = true;
        isReviving = false;
        gameObject.GetComponent<PlayerController>().enabled = true;
    }

}

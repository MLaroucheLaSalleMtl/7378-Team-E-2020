using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerStat : CharacterStat //Player Revive and Die handle
{
    public int playerLife = 2;
    [SerializeField] private float delayRevive = 5f;
    [SerializeField] private GameObject reviveHealthBar = null;
    [SerializeField] private Image life1 = null;
    [SerializeField] private Image life2 = null;
    [SerializeField] private Sprite death1 = null;
    [SerializeField] private Sprite death2 =null;
    private bool isReviving = false;


    public override void SetHealthBar(Slider slider)
    {
        base.SetHealthBar(slider);
    }

    public override void Die()
    {
        base.Die();
        PlayerDie();
    }

    private void PlayerDie()
    {
        playerLife--;
        if (playerLife >= 1)
        {
            life1.sprite = death1;
            StartCoroutine("Revive");
        }
        
        if(playerLife < 1 && !isReviving)
        {
            life2.sprite = death2;
            gameObject.GetComponent<CharacterAnimation>().AnimationDie();
            alive = false;
        }
    }

    IEnumerator Revive()
    {
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        gameObject.GetComponent<CharacterAnimation>().AnimationFaint();
        DisableOnDie();
        Invoke("Refill", 3f);
        yield return new WaitForSeconds(delayRevive);
        reviveHealthBar.SetActive(false);
        SetHp();
        RefillHealthBar();
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
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

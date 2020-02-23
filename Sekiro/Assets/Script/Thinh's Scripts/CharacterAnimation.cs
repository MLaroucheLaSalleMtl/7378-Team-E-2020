using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimation : MonoBehaviour
{
    private Animator anim;
    private PlayerController player;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<PlayerController>();
        
    }

    
    public void MoveAnimation(float horizontal, float vertical)
    {
        anim.SetFloat("Speed", Mathf.Abs(horizontal) + Mathf.Abs(vertical));
    }

    public void StateAnimation(int playerState)
    {
        anim.SetInteger("State", playerState);
    }

    public void FallAnimation(bool isFalling)
    {
        anim.SetBool("Fall", isFalling);
    }

    public void JumpAnimation()
    {
        anim.SetTrigger("Jump");
    }

    public void DeflectAnimation(bool isDeflecting)
    {
        anim.SetBool("Defend", isDeflecting);
    }

    public void IdleAnimation(bool isGrounded)
    {
        anim.SetBool("isGrounded", isGrounded);
    }


    public void AnimationAttack1()
    {
        anim.SetTrigger("Attack 1");
    }

    public void AnimationAttack2()
    {
        anim.SetTrigger("Attack 2");
    }

    public void AnimationAttack3()
    {
        anim.SetTrigger("Attack 3");
    }

    public void AnimationPowerUp()
    {
        anim.SetTrigger("PowerUp");
    }

    public void AnimationWithdrawSword()
    {
        anim.SetLayerWeight(1, 1f);
        anim.SetTrigger("SwordEquip");
        Invoke("ResetSwordLayerAnimation", 1.5f);
    }

    private void ResetSwordLayerAnimation()
    {
        anim.SetLayerWeight(1, 0);
    }

}

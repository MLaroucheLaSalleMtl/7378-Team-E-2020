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

    public void MoveAnimationBlendTree(float h, float v)
    {
        anim.SetFloat("H", h);
        anim.SetFloat("V", v);
    }

    public void StateAnimation(bool isUnarmed)
    {
        anim.SetBool("isUnarmed", isUnarmed);
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

   public void AnimationAttack1(bool check)
   {
        anim.SetBool("Attack 1", check);
   }

    public void AnimationAttack2(bool check)
    {
        anim.SetBool("Attack 2", check);
    }

    public void AnimationAttack3(bool check)
    {
        anim.SetBool("Attack 3", check);
    }

    public void AnimationThrowKunai(bool check)
    {
        anim.SetBool("hasKunai", check);
    }

    public void AnimationPowerUp()
    {
        anim.SetTrigger("PowerUp");
    }

    public void AnimationDie()
    {
        anim.SetTrigger("Die");
    }

    public void AnimationFaint()
    {
        anim.SetTrigger("Faint");
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

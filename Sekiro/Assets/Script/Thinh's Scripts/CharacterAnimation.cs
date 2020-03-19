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

    #region(Combo Attack)
    //public void AnimationAttack1()
    //{
    //    anim.SetTrigger("Attack 1");
    //}

    //public void AnimationAttack2()
    //{
    //    anim.SetTrigger("Attack 2");
    //}

    //public void AnimationAttack3()
    //{
    //    anim.SetTrigger("Attack 3");
    //}

    //public void AnimationAttack(int index)
    //{
    //    if(index == 0)
    //    {
    //        anim.SetTrigger("Attack 1");
    //    }
    //    else if(index == 1)
    //    {
    //        anim.SetTrigger("Attack 2");
    //    }
    //    else if(index == 2)
    //    {
    //        anim.SetTrigger("Attack 3");
    //    }
    //}
    #endregion

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

    public void AnimationThrowShuriken()
    {
        anim.SetLayerWeight(2, 1f);
        anim.SetTrigger("Throw");
        Invoke("ResetSkillLayer", 3f);
    }

    private void ResetSkillLayer()
    {
        anim.SetLayerWeight(2, 0);
    }

}

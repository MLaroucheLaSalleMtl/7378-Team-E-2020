using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CharacterAttack : MonoBehaviour
{

    private CharacterAnimation char_anim;
    private StealthKill sk;
    private bool isAttacking = false;
    private bool isReadyToAttack = true;
    private int noOfClicks = 0;
    private float lastClick = 0f;
    [Range(0, 3f)]public float comboAttackDelay = 0.9f;
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (sk.canTakeDown)
            {
                sk.TakeDown();
                isAttacking = true;
            }
            else
            {
                if (isReadyToAttack)
                {
                    lastClick = Time.time;
                    noOfClicks++;
                    if (noOfClicks == 1)
                    {
                        char_anim.AnimationAttack1(true);
                        isAttacking = true;
                    }
                    noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);
                    gameObject.GetComponent<PlayerController>().StopMoveSpeed();
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        char_anim = GetComponent<CharacterAnimation>();
        sk = GetComponent<StealthKill>();
    }

    // Update is called once per frame
    void Update()
    {

        PlayerAttack();
    }


    public bool CheckIsAttacking()
    {
        return isAttacking;
    }

    private void PlayerAttack()
    {
        if (Time.time - lastClick > comboAttackDelay)
        {
            noOfClicks=0;
            isAttacking = false;
            gameObject.GetComponent<PlayerController>().ResetMoveSpeed();
            gameObject.GetComponent<VFXActivation>().SwordTrailOff();
            gameObject.GetComponent<AttackCollisionControl>().DeactivateSwordAttackCollision();
        }
        if(!isAttacking)
        {
            ReturnAttack3();
        }
        
    }

    public void ReturnAttack1()
    {
        if(noOfClicks >= 2)
        {
            char_anim.AnimationAttack2(true);
        }
        else
        {
            char_anim.AnimationAttack1(false);
            noOfClicks = 0;
        }
    }

    public void ReturnAttack2()
    {
        if (noOfClicks >= 3)
        {
            char_anim.AnimationAttack3(true);
        }
        else
        {
            char_anim.AnimationAttack2(false);
            noOfClicks = 0;
        }
    }

    public void ReturnAttack3()
    {
        char_anim.AnimationAttack1(false);
        char_anim.AnimationAttack2(false);
        char_anim.AnimationAttack3(false);
        noOfClicks = 0;
    }

    public void AbleToAttack()
    {
        isReadyToAttack = true;
    }

    public void UnableToAttack()
    {
        isReadyToAttack = false;
    }
}

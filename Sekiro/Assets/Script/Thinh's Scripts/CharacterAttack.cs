using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public enum AttackState
{
    none,
    attack1,
    attack2,
    attack3
}


public class CharacterAttack : MonoBehaviour
{

    private CharacterAnimation char_anim;
    private float default_combo_timer = 0.4f;
    private float current_combo_timer = 0f;
    private bool combo_timer_reset;
    private bool isAttacking = false;
    private AttackState attack_state;


    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isAttacking = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        char_anim = GetComponent<CharacterAnimation>();
        attack_state = AttackState.none;
        current_combo_timer = default_combo_timer;
    }

    // Update is called once per frame
    void Update()
    {
        ComboAttack();
        ResetAttackCombo();
    }

    private void ComboAttack()
    {
        if (isAttacking)
        {

             if(attack_state == AttackState.attack3)
            {
                return;
            }
            isAttacking = false;
            attack_state++;
            combo_timer_reset = true;
            current_combo_timer = default_combo_timer;
            if(attack_state == AttackState.attack1)
            {
                char_anim.AnimationAttack1();
            }
            if (attack_state == AttackState.attack2)
            {
                char_anim.AnimationAttack2();
            }
            if (attack_state == AttackState.attack3)
            {
                char_anim.AnimationAttack3();
            }
        }
    }

    private void ResetAttackCombo()
    {
        if (combo_timer_reset)
        {
            current_combo_timer -= Time.deltaTime;
            if (current_combo_timer <= 0f)
            {
                attack_state = AttackState.none;
                combo_timer_reset = false;
                current_combo_timer = default_combo_timer;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum State
{
    SwordAttaching,
    Unarmed
}

[RequireComponent(typeof(UnityEngine.CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public class ControlSpamClick
    {
        public float currentTimer;
        public float defaultTimer;

        public ControlSpamClick(float ct, float dt)
        {
            currentTimer = ct;
            defaultTimer = dt;
        }

        public void StartTimer()
        {
            if (currentTimer == 0f)
            {
                currentTimer = defaultTimer;
            }
        }

        public void ResetTimer()
        {
            if (currentTimer > 0)
            {
                currentTimer -= Time.deltaTime;
            }
            else
            {
                currentTimer = 0f;
            }
        }

        public bool CheckTimer()
        {
            return (currentTimer == defaultTimer);
        }
    }

    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private float gravityForce = 0f;
    [SerializeField] private float jumpForce = 0f;

    [Space]
    [Header("Katana")]
    [SerializeField] private Transform katanaSpotOnHand = null;
    [SerializeField] private Transform katanaSpotInCover = null;
    [SerializeField] private GameObject katana = null;

    private CharacterAnimation char_anim;
    private Rigidbody rb;
    private float h = 0f;
    private float v = 0f;
    private UnityEngine.CharacterController character_controller;
    private Vector3 player_velocity;
    private bool jumping = false;
    private bool isEquipingSword = false;
    private bool isDeflecting = false;
    private bool isFalling = false;
    private State player_state = State.Unarmed;
    private bool is_unarmed_anim = true;

    private ControlSpamClick equipControl = new ControlSpamClick(0f, 1.5f);
    private ControlSpamClick jumpControl = new ControlSpamClick(0f, .5f);


    #region(Input System)
 
    //Input System Methods
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        h = value.x;
        v = value.y;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jumpControl.StartTimer();
        }

    }

    public void OnHoldLShift(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isDeflecting = true;
        }
        else if (context.canceled)
        {
            isDeflecting = false;
        }
    }

    public void OnPowerUp(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            char_anim.AnimationPowerUp();
        }

    }


    public void OnEquip(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            equipControl.StartTimer();
        }

    }
    #endregion //Input System

    // Start is called before the first frame update
    void Start()
    {
        char_anim = GetComponent<CharacterAnimation>();
        character_controller = GetComponent<UnityEngine.CharacterController>();
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        UpdateState();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        UpdateState();
        equipControl.ResetTimer();
        jumpControl.ResetTimer();
    }


    private void UpdateState()
    {
        if (isEquipingSword)
        {
            katana.transform.position = katanaSpotOnHand.position;
            katana.transform.rotation = katanaSpotOnHand.rotation;
            katana.transform.parent = katanaSpotOnHand;
        }
        else
        {
            katana.transform.position = katanaSpotInCover.position;
            katana.transform.rotation = katanaSpotInCover.rotation;
            katana.transform.parent = katanaSpotInCover;
        }

        if (equipControl.CheckTimer())
        {
            if (player_state == State.SwordAttaching)
            {
                char_anim.AnimationWithdrawSword();
            }

            else if (player_state == State.Unarmed)
            {
                char_anim.AnimationWithdrawSword();
            }

        }
    }

    public void Sword_Equip()
    {
        isEquipingSword = true;
        player_state = State.SwordAttaching;
        is_unarmed_anim = false;
    }

    public void Sword_Unequip()
    {
        isEquipingSword = false;
        player_state = State.Unarmed;
        is_unarmed_anim = true;
    }

    
    private void PlayerMove()
    {
        Vector3 player_move = transform.right * h + transform.forward * v;

        if (player_velocity.y <= 0 && character_controller.isGrounded)
        {
            player_velocity.y = -2f;
            if (player_velocity.y > -3f)
            {
                isFalling = false;
            }
        }
        else if (player_velocity.y < -4f && !character_controller.isGrounded)
        {
            isFalling = true;
        }
        //Jump
        if (character_controller.isGrounded)
        {
            player_velocity.y = 0f;
            if (jumpControl.CheckTimer())
            {
                player_velocity.y = jumpForce;
                char_anim.JumpAnimation();
            }
            jumpControl.currentTimer = 0f;
        }

        //Move player
        character_controller.Move(player_move.normalized * moveSpeed * Time.deltaTime);

        //Apply gravity
        player_velocity.y -= gravityForce * Time.deltaTime;
        character_controller.Move(player_velocity * Time.deltaTime);

        AnimationControl();
    }

    public void AnimationControl()
    {
        char_anim.StateAnimation(is_unarmed_anim);
        char_anim.FallAnimation(isFalling);
        char_anim.DeflectAnimation(isDeflecting);
        char_anim.IdleAnimation(character_controller.isGrounded);
        char_anim.MoveAnimation(h, v);
    }
}


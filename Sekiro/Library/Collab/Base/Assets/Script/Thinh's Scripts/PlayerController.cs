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
    private bool isEquiping = false;
    private State player_state = State.Unarmed;
    private int player_state_anim = 1;

   


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
            jumping = true;
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
            isEquiping = true;
        }
        
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
        
        if (isEquiping)
        {
            if (player_state == State.SwordAttaching)
            {
                player_state = State.Unarmed;
                isEquiping = false;
                player_state_anim = 1;
                char_anim.AnimationWithdrawSword();
            }

            else if (player_state == State.Unarmed)
            {
                player_state = State.SwordAttaching;
                isEquiping = false;
                player_state_anim = 0;
                char_anim.AnimationWithdrawSword();
            }
        }
    }
   
    public void Sword_Equip()
    {
        isEquipingSword = true;
    }

    public void Sword_Unequip()
    {
        isEquipingSword = false;
    }

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
            if (jumping)
            {
                player_velocity.y = jumpForce;
                char_anim.JumpAnimation();
            }
            jumping = false;
        }

        //Move player
        character_controller.Move(player_move.normalized* moveSpeed * Time.deltaTime);

        //Apply gravity
        player_velocity.y -= gravityForce * Time.deltaTime;
        character_controller.Move(player_velocity * Time.deltaTime);


        char_anim.StateAnimation(player_state_anim);
        char_anim.FallAnimation(isFalling);
        char_anim.DeflectAnimation(isDeflecting);
        char_anim.IdleAnimation(character_controller.isGrounded);
        char_anim.MoveAnimation(h, v);
    }

    public State ReturnPlayerState()
    {
        return player_state;
    }

    private void EquipSword()
    {
        katana.SetActive(isEquipingSword);
    }
    
}


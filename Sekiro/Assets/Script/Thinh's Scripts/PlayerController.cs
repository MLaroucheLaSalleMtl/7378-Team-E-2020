using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.InputSystem;

public enum State
{
    SwordAttaching,
    Unarmed,
}

[RequireComponent(typeof(UnityEngine.CharacterController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
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

    [Space]
    [Header("Jump")]
    [SerializeField] private float gravityForce = 0f;
    [SerializeField] private float jumpForce = 0f;
    [SerializeField] private float fallMultiplier = 0f;
    [SerializeField] private float lowJumpMultiplier = 0f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform checkGroundPos = null;
    [SerializeField] private float checkGroundRadius = 0f;
    private bool isGrounded;

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
    private bool isEquipingSword = false;
    private bool isAttacking = false;
    private bool isDeflecting = false;
    private bool isFalling = false;
    private bool isJumping = false;
    private State player_state = State.Unarmed;
    private bool is_unarmed_anim = true;
    [SerializeField] private NPCState npcState;

    private ControlSpamClick equipControl = new ControlSpamClick(0f, 1.5f);
    private ControlSpamClick jumpControl = new ControlSpamClick(0f, .3f);


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
        isJumping = context.performed;

    }

    public void OnHoldLShift(InputAction.CallbackContext context)
    {
        isDeflecting = context.performed;
    }


    public void OnEquip(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!isAttacking)
            {
                equipControl.StartTimer();
                GetComponent<EquipItem>().ReturnToNone();
            }
        }

    }
    #endregion //Input System

    // Start is called before the first frame update
    void Start()
    {
        char_anim = GetComponent<CharacterAnimation>();
        character_controller = GetComponent<UnityEngine.CharacterController>();
        rb = GetComponent<Rigidbody>();
        UpdateState();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        UpdateState();
        ChangeNPCState();
        equipControl.ResetTimer();
        jumpControl.ResetTimer();
        CheckIsGrounded();
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
        isAttacking = gameObject.GetComponent<CharacterAttack>().CheckIsAttacking();
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

    private void CheckIsGrounded()
    {
        isGrounded = Physics.CheckSphere(checkGroundPos.position, checkGroundRadius, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(checkGroundPos.position, checkGroundRadius);
    }

    private void PlayerMove()
    {
        Vector3 player_move = transform.right * h + transform.forward * v;
        //Vector3 player_move = new Vector3(h, 0, v);
        BetterJump();
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
            if (jumpControl.CheckTimer() && !gameObject.GetComponent<CharacterAttack>().CheckIsAttacking())
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

    private void BetterJump()
    {
        if(player_velocity.y < 0)
        {
            player_velocity += Vector3.up * gravityForce * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if(player_velocity.y >0 && !isJumping)
        {
            player_velocity += Vector3.up * gravityForce * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void ChangeNPCState()
    {
        if (isDeflecting)
        {
            npcState = NPCState.Defend;
        }
        else
        {
            npcState = NPCState.Idle;
        }
    }

    public NPCState GetCurrentState() => npcState;

    public State GetCurrentPlayerState() => player_state;

    public void StopMoveSpeed()
    {
        if(moveSpeed > 0)
        {
            moveSpeed = 0f;
        }
    }

    public void ResetMoveSpeed()
    {
        if(moveSpeed <= 0)
        {
            moveSpeed = .2f;
        }
    }

    public bool HealSkill()
    {
        if (GetComponent<PlayerStat>().CanHeal())
        {
            char_anim.AnimationPowerUp();
            return true;
        }
        else { return false; }
    }


    public void AnimationControl()
    {
        char_anim.MoveAnimationBlendTree(h, v);
        char_anim.StateAnimation(is_unarmed_anim);
        char_anim.FallAnimation(isFalling);
        char_anim.DeflectAnimation(isDeflecting);
        char_anim.IdleAnimation(isGrounded);
        char_anim.MoveAnimation(h, v);
    }
}


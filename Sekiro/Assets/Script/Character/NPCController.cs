using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AIController))]
[RequireComponent(typeof(AIFOV))]
[RequireComponent(typeof(EnemyStat))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(NPCHitDetector))]
public class NPCController : MonoBehaviour
{
    [SerializeField] float movingTurnSpeed = 360;
    [SerializeField] float stationaryTurnSpeed = 180;
    [SerializeField] float m_JumpPower = 12f;
    [Range(1f, 4f)] [SerializeField] float m_GravityMultiplier = 2f;
    [SerializeField] float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
    [SerializeField] float m_MoveSpeedMultiplier = 1f;
    [SerializeField] float m_AnimSpeedMultiplier = 1f;
    [SerializeField] float m_GroundCheckDistance = 0.1f;
    [SerializeField] List<GameObject> items;

    public bool useStrafeControl;

    AIController ai;
    Animator animator;
    Rigidbody rigidbody;
    CapsuleCollider capsule;
    Vector3 groundNormal;

    Vector3 capsuleCenter;
    float capsuleHeight;
    float turnAmount;
    float forwardAmount;
    float turnSpeed;

    bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<AIController>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        items[0].gameObject.SetActive(false);
    }

    public void FaceTarget(Vector3 target)
    {
        Vector3 direction;

        direction = (target - transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public Animator GetAnimator() => animator;

    void ApplyExtraTurnRotation()
    {
        // help the character turn faster (this is in addition to root rotation in the animation)
        turnSpeed = Mathf.Lerp(stationaryTurnSpeed, movingTurnSpeed, forwardAmount);
        transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);
    }

    void UpdateAnimator(Vector3 move)
    {
        isGrounded = true;
        animator.SetFloat("Forward", forwardAmount, 0.1f, Time.deltaTime);
        animator.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
    }

    IEnumerator WaitABit(float delay, bool withdrawSword, bool shealthSword)
    {
        yield return new WaitForSeconds(delay);
        items[2].gameObject.SetActive(shealthSword);
        items[0].gameObject.SetActive(withdrawSword);
    }


    public void WithdrawWeapon()
    {
        ExcuteBoolAnimation("ShealthSword", false);
        ExcuteBoolAnimation("WithdrawingSword", true);
        ExcuteBoolAnimation("SuspectedObject", true);
        StartCoroutine(WaitABit(0.6f, true, false));
    }

    public void ShealthSword()
    {
        ExcuteBoolAnimation("SuspectedObject", false);
        ExcuteBoolAnimation("WithdrawingSword", false);
        ExcuteBoolAnimation("ShealthSword", true);
        StartCoroutine(WaitABit(1.4f, false, true));
    }

    public void PlayAnimation(string animation) => animator.Play(animation);

    public void ExcuteBoolAnimation(string animation, bool value) => animator.SetBool(animation, value);

    public void ExcuteTriggerAnimation(string animation) => animator.SetTrigger(animation);

    public void ExcuteAttack(string aniamtion, bool isAttack)
    {

    }

    public void Stop()
    {
        animator.SetFloat("Turn", 0);
        animator.SetFloat("Forward", 0);
    }

    public void Move(Vector3 move, bool crouch, bool jump)
    {
        if (move.magnitude > 1f) move.Normalize();
        move = transform.InverseTransformDirection(move);
        
        move = Vector3.ProjectOnPlane(move, groundNormal);
        turnAmount = Mathf.Atan2(move.x, move.z) * Time.deltaTime * 10f;
        forwardAmount = move.z;

        if (useStrafeControl)
        {
            if (forwardAmount < 0.0f)
            {
                //turnSpeed = ai.Agent.speed;

                turnSpeed = Mathf.Clamp(turnSpeed, -1.0f, 1.0f);
                //forwardSpeed = Mathf.Clamp(turnSpeed, -1.0f, 1.0f);
                transform.Rotate(0.0f, turnSpeed, 0.0f);
                transform.Rotate(0.0f, 0.0f, move.z);

                //Behavior
                //m_Animator.SetFloat("Forward", move.z);
                //m_Animator.SetFloat("Turn", turnSpeed);
            }
        }
        else
            ApplyExtraTurnRotation();
        // send input and other state parameters to the animator

        UpdateAnimator(move);
    }
}

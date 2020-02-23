using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    private NPCController myController;
    private CharacterStat myStat;
    private AIFOV mySight;
    private NPCHitDetector myHitDetector;
    private NavMeshAgent agent;
    [SerializeField]
    private WeaponHitDetector myWeaponHitDetector;
    [SerializeField]
    private NPCState state;
    [SerializeField]
    private Transform pathHolder;
    [SerializeField]
    private Vector3[] waypoints;
    [SerializeField]
    private int currentWaypoint = 0;
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private float stoppingDistance = .1f;
    [SerializeField]
    [Range(0.0f, 1.0f)] private float patrolSpeed = 0.5f;

    [SerializeField]
    [Range(0.0f, 1.0f)] private float chaseSpeed = 1f;
    [SerializeField]
    [Range(0f, 5f)] private float waitTime = 3f;

    public NavMeshAgent Agent { get => agent; }

    public bool isAttacking;
    public bool isDefending;
    private void OnDrawGizmos()
    {
        Transform startPosition = pathHolder.GetChild(0);
        Transform previousPosition = startPosition;

        foreach (Transform waypoint in pathHolder)
        {
            Gizmos.DrawSphere(waypoint.position, .3f);
            Gizmos.DrawLine(previousPosition.position, waypoint.position);
            previousPosition = waypoint;
        }
        Gizmos.DrawLine(previousPosition.position, startPosition.position);
    }

    private void Start()
    {
        waypoints = new Vector3[pathHolder.childCount];

        for (int i = 0; i < waypoints.Length; i++)
            waypoints[i] = pathHolder.GetChild(i).position;

        agent = GetComponent<NavMeshAgent>();
        myController = GetComponent<NPCController>();
        myStat = GetComponent<CharacterStat>();
        mySight = GetComponent<AIFOV>();
        myHitDetector = GetComponent<NPCHitDetector>();

        agent.updatePosition = true;
        agent.updateRotation = true;
        agent.autoBraking = false;
        transform.position = waypoints[currentWaypoint];
        state = NPCState.Patrol;

        //Wait a little bit before moving
        Invoke("ActivateFSM", 1f);
    }

    void ActivateFSM() => StartCoroutine("FSM");

    IEnumerator OnChase()
    {
        myController.ExcuteBoolAnimation("InFight", false);
        agent.speed = chaseSpeed;

        agent.stoppingDistance = 1f;

        while (state == NPCState.Chase)
        {
            myController.Move(agent.desiredVelocity, false, false);

            if (mySight.CanSeePlayer())
            {
                if (mySight.player != null)
                {
                    target = mySight.player;
                    myController.FaceTarget(target.transform.position);
                    agent.SetDestination(target.transform.position);

                    if (Vector3.Distance(transform.position, target.transform.position) <= 3f)
                    {
                        state = NPCState.Fight;
                        yield return new WaitForSeconds(.1f);
                        break;
                    }
                }
            }
            else
            {
                myController.Stop();
                myController.FaceTarget(transform.forward);
                agent.SetDestination(transform.position); ;
                state = NPCState.Caution;
                yield return new WaitForSeconds(2f);
                break;
            }
            yield return null;
        }
    }

    IEnumerator OnCaution()
    {
        agent.speed = patrolSpeed;

        agent.stoppingDistance = stoppingDistance;

        while (state == NPCState.Caution)
        {
            myController.Move(agent.desiredVelocity, false, false);

            if (!mySight.CanSeePlayer())
            {
                myController.Stop();

                myController.ShealthSword();

                agent.SetDestination(transform.position);

                state = NPCState.Patrol;
                yield return new WaitForSeconds(2f);
                break;

            }
            yield return null;
        }
    }

    IEnumerator OnPatrol()
    {
        myController.useStrafeControl = false;
        agent.stoppingDistance = stoppingDistance;
        agent.speed = patrolSpeed;

        if (waypoints.Length == 0) yield return null;

        while (state == NPCState.Patrol)
        {
            myController.Move(agent.desiredVelocity, false, false);

            if (mySight.CanSeePlayer())
            {
                myController.Stop();
                agent.SetDestination(transform.position);
                myController.WithdrawWeapon();

                state = NPCState.Chase;
                yield return new WaitForSeconds(1f);
                break;
            }

            if (Vector3.Distance(transform.position, waypoints[currentWaypoint]) > 2.0f)
            {
                agent.autoBraking = false;
                agent.SetDestination(waypoints[currentWaypoint]);
                myController.Move(agent.desiredVelocity, false, false);
            }

            if (Vector3.Distance(transform.position, waypoints[currentWaypoint]) <= 2.0f)
            {
                if (agent.remainingDistance > 0.0f && agent.remainingDistance <= 0.1f)
                {
                    agent.autoBraking = true;
                    myController.Move(Vector3.zero, false, false);
                }

                if (agent.remainingDistance <= 0.0f)
                {
                    currentWaypoint++;

                    if (currentWaypoint >= waypoints.Length)
                        currentWaypoint = 0;

                    myController.FaceTarget(waypoints[currentWaypoint]);
                    myController.Move(agent.desiredVelocity, false, false);
                    yield return new WaitForSeconds(waitTime);
                    break;
                }
            }
            yield return null;
        }
    }

    IEnumerator OnAttack()
    {
        myWeaponHitDetector.numberOfHits = 0;
        myWeaponHitDetector.isHit = false;
        myController.ExcuteBoolAnimation("Attack", false);
        myController.Move(agent.desiredVelocity, false, false);
        agent.SetDestination(transform.position);

        while (state == NPCState.Attack)
        {
            if (Vector3.Distance(transform.position, target.transform.position) > 3f || !mySight.CanSeePlayer())
            {
                state = NPCState.Chase;
            }
            else
            {
                if(!myHitDetector.isHit)
                {
                    myController.ExcuteBoolAnimation("Attack", true);
                    float delayTime = Random.Range(1, 3f);
                    yield return new WaitForSeconds(delayTime);
                    myController.ExcuteBoolAnimation("Attack", false);
                    state = NPCState.Fight;
                }
            }
            yield return null;
        }
    }

    IEnumerator OnDefend()
    {
        myWeaponHitDetector.numberOfHits = 0;
        myWeaponHitDetector.isHit = false;

        myController.Move(agent.desiredVelocity, false, false);
        agent.SetDestination(transform.position);
        while (state == NPCState.Defend)
        {

            yield return null;
        }
    }

    IEnumerator OnFight()
    {
        agent.speed = patrolSpeed;
        myController.ExcuteBoolAnimation("InFight", true);
        agent.stoppingDistance = 1f;
        myController.useStrafeControl = true;
        //int decision = Random.Range(0, 2);
        //target.gameObject.GetComponent<PlayerStat>().isAlive -- While condition later on
        while (true)
        {
            //Debug.Log(Vector3.Distance(transform.position, target.transform.position));
            //if (Vector3.Distance(transform.position, target.transform.position) > 2f)
            Debug.Log(agent.remainingDistance);
            if (agent.remainingDistance > 1.9f)
            {
                state = NPCState.Chase;
                yield return new WaitForSeconds(.1f);
            }
            else
            {
                Debug.Log("Oh My God");
                agent.speed = patrolSpeed;
                myController.Stop();
                agent.SetDestination(transform.position);
                agent.autoBraking = true;

                if (!myHitDetector.isHit)
                {
                    state = NPCState.Attack;
                    int delayTime = Random.Range(1, 3);
                    yield return new WaitForSeconds(delayTime);
                    break;
                }

                if (myHitDetector.isHit)
                {
                    state = NPCState.Defend;
                    yield return new WaitForSeconds(.1f);
                    break;
                }
            }
            yield return null;
        }
    }

    IEnumerator FSM()
    {
        while (myStat.alive)
        {
            switch (state)
            {
                case NPCState.Patrol:
                    Debug.Log("I am here OnPatrol");
                    yield return StartCoroutine(OnPatrol());
                    break;
                case NPCState.Caution:
                    Debug.Log("I am here OnCaution");
                    yield return StartCoroutine(OnCaution());
                    break;
                case NPCState.Chase:
                    Debug.Log("I am here OnChase");
                    yield return StartCoroutine(OnChase());
                    break;
                case NPCState.Fight:
                    Debug.Log("I am here OnFight");
                    yield return StartCoroutine(OnFight());
                    break;
                case NPCState.Attack:
                    Debug.Log("I am here OnAttack");
                    isAttacking = true;
                    yield return StartCoroutine(OnAttack());
                    break;
            }
            yield return null;
        }
    }
}

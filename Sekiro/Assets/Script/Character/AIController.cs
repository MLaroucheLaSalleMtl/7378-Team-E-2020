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


    private NavMeshAgent agent;

    [SerializeField]
    private NPCState state;
    [SerializeField]
    private Transform pathHolder;
    [SerializeField]
    private Vector3[] waypoints;
    [SerializeField]
    private int currentWaypoint = 0;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float stoppingDistance = .1f;
    [SerializeField]
    [Range(0.0f, 1.0f)] private float patrolSpeed = 0.5f;
    [SerializeField]
    [Range(0f, 5f)] private float waitTime = 3f;
    [SerializeField]
    private float chaseSpeed = 1f;

    public NavMeshAgent Agent { get => agent; }

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

        agent.updatePosition = true;
        agent.updateRotation = true;
        agent.autoBraking = false;

        state = NPCState.Patrol;

        //Wait a little bit before moving
        StartCoroutine(FSM());
    }
    public void FaceTarget()
    {
        Vector3 direction;

        if (target != null)
        {
            direction = (target.transform.position - transform.position).normalized;

            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    private void Update()
    {

    }

    void ActivateFSM() => StartCoroutine("FSM");


    IEnumerator OnChase()
    {
        agent.speed = chaseSpeed;

        myController.WithdrawWeapon();

        agent.stoppingDistance = 2f;


        while (state == NPCState.Chase)
        {
            myController.Move(agent.desiredVelocity, false, false);
            if (mySight.CanSeePlayer())
                agent.SetDestination(mySight.player.transform.position);
            else
            {
                myController.Stop();
                agent.SetDestination(transform.position);
                yield return new WaitForSeconds(.1f);
                state = NPCState.Caution;
            }

            yield return null;
        }
    }

    IEnumerator OnCaution()
    {
        agent.speed = patrolSpeed;

        agent.stoppingDistance = stoppingDistance;

        myController.Move(agent.desiredVelocity, false, false);
        
        while (state == NPCState.Caution)
        {
            if (!mySight.CanSeePlayer())
            {
                myController.Stop();

                myController.ShealthSword();

                agent.SetDestination(transform.position);

                yield return new WaitForSeconds(waitTime);

                state = NPCState.Patrol;
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
                yield return new WaitForSeconds(.1f);

                state = NPCState.Chase;
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

                    yield return new WaitForSeconds(waitTime);
                    myController.Move(agent.desiredVelocity, false, false);
                }
            }
            yield return null;
        }
    }

    IEnumerator FSM()
    {
        while (myStat.isAlive)
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
            }
            yield return null;
        }
    }
}

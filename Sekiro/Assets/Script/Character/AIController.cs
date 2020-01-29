using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField]
    private float fieldOfViewAngle = 110f;
    [SerializeField]
    private bool playerInSight;
    [SerializeField]
    private Vector3 personalLastSighting;
    private NavMeshAgent agent;
    private SphereCollider collider;
    //private Las lastPlayerSighting;
    private GameObject player;
    private Vector3 previousSighting;

    private NPCController character;
    private CharacterStat thisStats;

    public NPCState state;

    [SerializeField]
    private List<GameObject> waypoints;
    [SerializeField]
    private int currentWaypoint;
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private float patrolSpeed = 0.5f;
    [SerializeField]
    private float chaseSpeed = 1f;

    public NavMeshAgent Agent { get => agent;}

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        character = GetComponent<NPCController>();
        thisStats = GetComponent<CharacterStat>();

        collider = GetComponent<SphereCollider>();

        agent.updatePosition = true;
        agent.updateRotation = false;

        state = NPCState.Idle;

        StartCoroutine("FSM");
    }

    private void Update()
    {
    }

    void OnIdle()
    {
        state = NPCState.Idle;
        Invoke("OnPatrol", 1f);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        currentWaypoint++;
    }

    void OnPatrol()
    {
        state = NPCState.Patrol;
        agent.stoppingDistance = 0f;
        agent.speed = patrolSpeed;

        if (waypoints.Count() == 0) return;

        if (Vector3.Distance(transform.position, waypoints[currentWaypoint].transform.position) > 2)
        {
            agent.SetDestination(waypoints[currentWaypoint].transform.position);
            character.Move(agent.desiredVelocity, false, false);
        }
        else if (Vector3.Distance(transform.position, waypoints[currentWaypoint].transform.position) <= 2)
        {
            currentWaypoint++;
            character.Move(agent.desiredVelocity, false, false);
            if (currentWaypoint >= waypoints.Count())
                currentWaypoint = 0;
        }
        else
            character.Move(Vector3.zero, false, false);

        /*
         * While patrolling
         *      if(spot an player)
         *          state = State.Chase;
         */
    }

    void OnChase()
    {
        agent.speed = chaseSpeed;
        //character.Move(agent.desiredVelocity, false, false);
        agent.stoppingDistance = 5f;

        //FaceTarget();

        agent.SetDestination(target.transform.position);
    }

    IEnumerator FSM()
    {
        while(thisStats.IsAlive)
        {
            switch(state)
            {
                case NPCState.Idle:
                    OnIdle();
                    break;
                case NPCState.Patrol:
                    OnPatrol();
                    break;
                case NPCState.Chase:
                    OnChase();
                    break;
            }
            yield return null;
        }
    }
}

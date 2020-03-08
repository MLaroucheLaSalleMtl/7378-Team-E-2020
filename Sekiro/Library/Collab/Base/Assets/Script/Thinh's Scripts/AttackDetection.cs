using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetection : MonoBehaviour
{
    [SerializeField] private LayerMask attackableLayer;
    [SerializeField] private float radius = 1f;
    [SerializeField] private CharacterCombat combat;
    [SerializeField]
    private CharacterStat targetStat;
    [SerializeField]
    private NPCState state;
    // Update is called once per frame
    private void FixedUpdate()
    {
        DetectCollision();
    }

    private void DetectCollision()
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, radius, attackableLayer);

        if (hit.Length > 0)
        {
            foreach (Collider h in hit)
            {
                targetStat = h.GetComponent<CharacterStat>();

                //Update this one
                //if (h.tag == "Player")
                //    //state = h.GetComponent<PlayerController>().g
                //    //Put your defend state here
                //else
                //    state = h.GetComponent<AIController>().GetCurrentState();


                //Temporary update the one above then remove this one
                state = h.GetComponent<AIController>().GetCurrentState();


                if (state == NPCState.Defend)
                    combat.Attack(null);
                else
                    combat.Attack(targetStat);
                // combat.Attack(h.GetComponent<CharacterStat>());
                // print("Hit the " + h.gameObject.name);
            }

            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

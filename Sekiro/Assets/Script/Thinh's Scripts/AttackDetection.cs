using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetection : MonoBehaviour
{
    [SerializeField] private LayerMask attackableLayer;
    [SerializeField] private float radius = 1f;
    [SerializeField] private CharacterCombat combat;
    private CharacterStat targetStat;
    [SerializeField] private NPCState state;

    [Space]
    [Header("VFX")]
    [SerializeField] private GameObject hitSwordVFX = null;
    [SerializeField] private GameObject bloodEffect = null;
    // Update is called once per frame
    private void FixedUpdate()
    {
        DetectCollision();
    }

    private void Start()
    {
        combat = gameObject.GetComponentInParent<CharacterCombat>();
    }

    private void DetectCollision()
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, radius, attackableLayer);

        if (hit.Length > 0)
        {
            foreach (Collider h in hit)
            {
                if(h != null)
                    targetStat = h.GetComponent<CharacterStat>();

                if (h.gameObject.CompareTag("Player"))
                {
                    state = h.GetComponent<PlayerController>().GetCurrentState();
                    print("Hit the player");
                }
               
                else if(h.gameObject.CompareTag("Enemy"))
                {
                    state = h.GetComponent<AIController>().GetCurrentState();
                   
                    //print("Hit the enemy");
                }

                if (state == NPCState.Defend)
                {
                    combat.Attack(null);
                    if (hitSwordVFX != null)
                    {
                        Destroy(Instantiate(hitSwordVFX, h.transform.position, Quaternion.identity) as GameObject, 1f);
                    }
                }
                else
                {
                    combat.Attack(targetStat);
                    if (bloodEffect != null)
                    {
                        Destroy(Instantiate(bloodEffect, transform.position, transform.rotation) as GameObject, 1f);
                    }
                }
                print("Hit the " + h.gameObject.name);
            }

            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

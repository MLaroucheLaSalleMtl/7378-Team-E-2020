using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetection : MonoBehaviour
{
    [SerializeField] private LayerMask attackableLayer;
    [SerializeField] private float radius = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectCollision();
    }

    private void DetectCollision()
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, radius, attackableLayer);

        if (hit.Length > 0)
        {
            print("Hit the " + hit[0].gameObject.name);

            gameObject.SetActive(false);
        }
    }
}

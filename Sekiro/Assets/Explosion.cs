using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float explosionRad = 0;
    [SerializeField] private float power = 0;
    [SerializeField] private float delay = 0;
    [SerializeField] private GameObject explosionEffect = null;
    public void Explode()
    {
        StartCoroutine("BombTimer");
    }

    IEnumerator BombTimer()
    {
        yield return new WaitForSeconds(delay);

        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRad);

        foreach(Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(power * 10, explosionPos, explosionRad);
            }
        }
        Destroy(gameObject);
        Destroy(Instantiate(explosionEffect, transform.position, transform.rotation) as GameObject,2f) ;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRad);
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float explosionRad = 0;
    [SerializeField] private float power = 0;
    [SerializeField] private int damage = 0;
    [SerializeField] private float delay = 0;
    [SerializeField] private GameObject explosionEffect = null;
    public void Explode()
    {
        Invoke("BombTimer", delay);
    }

    private void BombTimer()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRad);

        foreach(Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            CharacterStat es = hit.GetComponent<CharacterStat>();
            if (rb != null)
            {
                rb.AddExplosionForce(power * 10, explosionPos, explosionRad);
            }
            if (es != null)
            {
                es.TakeDamage(damage);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float explosionRad = 0;
    [SerializeField] private float power = 0;
    [SerializeField] private int damage = 0;
    [SerializeField] private float delay = 0;
    [SerializeField] private GameObject explosionEffect = null;
    [SerializeField] private AudioClip exPlosionSFX = null;
    private PlaySoundDestroy playDestroyAudio;

    private void Start()
    {
        playDestroyAudio = GetComponentInChildren<PlaySoundDestroy>();
    }
    public void Explode()
    {
        Invoke("BombTimer", delay);
    }

    private void BombTimer()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRad);

        foreach (Collider hit in colliders)
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
        playDestroyAudio.PlayOnDestroy(exPlosionSFX, delay);
        //Destroy(gameObject);
        Destroy(Instantiate(explosionEffect, transform.position, transform.rotation) as GameObject,3f) ;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRad);
    }
}

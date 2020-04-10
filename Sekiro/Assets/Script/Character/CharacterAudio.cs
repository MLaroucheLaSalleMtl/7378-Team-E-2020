using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CharacterAudio : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField] private AudioMixerSnapshot themeSnapshot;
    [SerializeField] private AudioMixerSnapshot fightingSnapshot;
    [SerializeField] private LayerMask enemyMask;
    bool enemyIsNear;
    public AudioClip[] throwSFX;
    public AudioClip withdrawSwordSFX;
    public AudioClip normalAttackSFX;
    public AudioClip dieSFX;
    public AudioClip swordSFX;
    public AudioClip hitSFX;
    public AudioClip jumpingSFX;
    private void Update()
    {
        RaycastHit[] rays = Physics.SphereCastAll(transform.position, 6f, transform.forward, 0f, enemyMask);
        if(rays.Length >0)
        {
            if (!enemyIsNear)
            {
                fightingSnapshot.TransitionTo(4f);
                enemyIsNear = true;
            }
        }
        else
        {
            enemyIsNear = false;
            themeSnapshot.TransitionTo(4f);
        }
    }

    public void WithDrawSwordSFX()
    {
        audioSource.PlayOneShot(withdrawSwordSFX);
    }
    public void ThrowingSFX()
    {
        audioSource.clip = throwSFX[Random.Range(0, throwSFX.Length)];
        audioSource.Play();
    }
    public void NormalAttackSFX()
    {
        audioSource.PlayOneShot(normalAttackSFX);
    }
    public void DieSFX()
    {
        audioSource.PlayOneShot(dieSFX);
    }
    public void SwordAttackSFX()
    {
        audioSource.PlayOneShot(swordSFX);
    }
    public void DamageSFX()
    {
        audioSource.PlayOneShot(hitSFX);
    }
    public void JumpSFX()
    {
        audioSource.PlayOneShot(jumpingSFX);
    }
}

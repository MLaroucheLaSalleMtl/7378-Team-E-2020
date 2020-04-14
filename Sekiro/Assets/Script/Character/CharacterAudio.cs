using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CharacterAudio : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField] private AudioMixerSnapshot fightingSnapshot;
    [SerializeField] private AudioMixerSnapshot ambientSnapshot;
    [SerializeField] private LayerMask enemyMask;
    private bool enemyIsNear;
    [SerializeField] private AudioClip[] throwSFX;
    [SerializeField] private AudioClip withdrawSwordSFX;
    [SerializeField] private AudioClip normalAttackSFX;
    [SerializeField] private AudioClip dieSFX;
    [SerializeField] private AudioClip swordSFX;
    [SerializeField] private AudioClip hitSFX;
    [SerializeField] private AudioClip jumpingSFX;
    [SerializeField] private AudioClip explodeSFX;
    [SerializeField] private AudioClip swordCut;
    [SerializeField] private AudioClip footstepSFX;
    private void Update()
    {
        RaycastHit[] rays = Physics.SphereCastAll(transform.position, 4f, transform.forward, 0f, enemyMask);
        if (rays.Length > 0)
        {
            if (!enemyIsNear)
            {
                fightingSnapshot.TransitionTo(4f);
                enemyIsNear = true;
            }
        }
        else
        {
            if(enemyIsNear)
            ambientSnapshot.TransitionTo(4f);
            enemyIsNear = false;
        }

    }

    public void WithDrawSwordSFX() =>
        audioSource.PlayOneShot(withdrawSwordSFX);
    public void ThrowingSFX()
    {
        audioSource.clip = throwSFX[Random.Range(0, throwSFX.Length)];
        audioSource.Play();
    }
    public void NormalAttackSFX() =>
        audioSource.PlayOneShot(normalAttackSFX,1);
    public void DieSFX() =>
        audioSource.PlayOneShot(dieSFX);
    public void SwordAttackSFX() =>
        audioSource.PlayOneShot(swordSFX);
    public void DamageSFX() =>
        audioSource.PlayOneShot(hitSFX);
    public void JumpSFX() =>
        audioSource.PlayOneShot(jumpingSFX);
    public void ExplodeSFX() =>
        audioSource.PlayOneShot(explodeSFX);

    public void SwordCut() =>
        audioSource.PlayOneShot(swordCut);

    private void FootStep() =>
        audioSource.PlayOneShot(footstepSFX);
}

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
}

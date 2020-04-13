using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundDestroy : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip audioToPlay;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayOnDestroy(AudioClip playClip, float delay)
    {
        audioToPlay = playClip;
        transform.parent = null;
        audioSource.PlayOneShot(audioToPlay);
        Destroy(gameObject, delay);
    }
}

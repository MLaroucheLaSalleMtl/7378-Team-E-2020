using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(AudioSource))]
public class FootSteps : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    // Start is called before the first frame update
    void Start() =>
        audioSource = GetComponent<AudioSource>(); 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingSystem : MonoBehaviour
{
    private Animator anim;
    private PlayerController player;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<PlayerController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Climbable"))
        {
            anim.SetTrigger("Climbing");
            Debug.Log("Ladder");
        }
    }
}

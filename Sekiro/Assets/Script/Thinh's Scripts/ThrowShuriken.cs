using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowShuriken : MonoBehaviour
{
    [SerializeField] private GameObject explosiveShuriken=null;
    [SerializeField] private Transform throwPos=null;
    [SerializeField] private float throwForce = 0;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ThrowKunai()
    {
        GameObject go = Instantiate(explosiveShuriken, throwPos.position, throwPos.rotation);
        Rigidbody rb = go.GetComponent<Rigidbody>();
        rb.velocity = go.transform.up * throwForce;
    }
}

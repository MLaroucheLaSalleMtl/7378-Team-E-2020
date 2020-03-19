using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowShuriken : MonoBehaviour
{
    [SerializeField] private GameObject shuriken;
    [SerializeField] private Transform throwPos;
    [SerializeField] private float throwForce = 0;

    public void OnThrow(InputAction.CallbackContext context)
    {
        if (context.started)
        {
          GameObject go =  Instantiate(shuriken, throwPos.position, throwPos.rotation);
          Rigidbody rb = go.GetComponent<Rigidbody>();
          rb.velocity = go.transform.up * throwForce;
        }
    }

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
        GameObject go = Instantiate(shuriken, throwPos.position, throwPos.rotation);
        Rigidbody rb = go.GetComponent<Rigidbody>();
        rb.velocity = go.transform.up * throwForce;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private Vector3 moveDir;
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDir * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameObject enemy = other.gameObject;
            enemy.GetComponent<Rigidbody>().AddForce(transform.up*5f, ForceMode.Impulse);
            
        }
    }
}

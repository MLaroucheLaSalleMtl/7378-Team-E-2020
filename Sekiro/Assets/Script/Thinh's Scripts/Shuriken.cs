using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private int damage = 0;
    private Vector3 shootDir;
    private Rigidbody rb;
    private bool hitSth = false;
    private bool dealDame = true;

    public void GetShootDir(Vector3 shootDir)
    {
        this.shootDir = shootDir;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            CharacterStat stat = collision.gameObject.GetComponent<CharacterStat>();
            if (stat != null && dealDame)
            {
                dealDame = false;
                stat.TakeDamage(damage);
            }
            hitSth = true;
            Stick(collision.transform);
            gameObject.GetComponent<TrailRenderer>().enabled = false;
            
        }
        gameObject.GetComponent<Explosion>().Explode();
    }

    private void Stick(Transform parent)
    {
        //transform.LookAt(parent);
        rb.constraints = RigidbodyConstraints.FreezeAll;
        transform.parent = parent;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Collectables : MonoBehaviour
{
 
    [SerializeField] private float rotateSpeed = 0;
    private Item item;
    // Start is called before the first frame update
    void Start()
    {
        item = GetComponent<Item>();
    }

    // Update is called once per frame
    void Update()
    {
        SelfRotate();
    }

    void SelfRotate()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PickUp();
        }
    }

    public virtual void PickUp()
    {
        Inventory.Instance.AddItem(item);
        Destroy(gameObject);
    }
}

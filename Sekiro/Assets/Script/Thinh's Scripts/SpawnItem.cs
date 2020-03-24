using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    [SerializeField] private GameObject[] items;
    private Vector3 offset = new Vector3(0, 0.5f, 0);
    
    public void DropItem()
    {
        if(items !=null)
        {
            foreach(GameObject go in items)
            {
                Instantiate(go, transform.position + offset, transform.rotation);
            }
        }
    }
}

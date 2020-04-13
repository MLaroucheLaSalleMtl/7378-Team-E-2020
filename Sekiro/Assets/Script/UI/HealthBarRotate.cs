using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarRotate : MonoBehaviour
{
    [SerializeField] private Camera myCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (myCamera !=null)
        {
            transform.eulerAngles = new Vector3(myCamera.transform.eulerAngles.x, myCamera.transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }
}

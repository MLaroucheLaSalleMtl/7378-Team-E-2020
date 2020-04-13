using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public GameObject timelineCam;
    public GameObject playerCam;
    public float timeToWait = 0f;
    // Start is called before the first frame update
    void Start()
    {
        playerCam.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timeToWait++;
        if (timeToWait >= 750)
        {
            timelineCam.SetActive(false);
            playerCam.SetActive(true);
            timeToWait--;
        }        
    }

  
}

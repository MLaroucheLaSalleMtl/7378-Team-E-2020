using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] private GameObject playerInput = null;
    public GameObject timelineCam;
    public GameObject playerCam;
    public float timeToWait = 0f;
    // Start is called before the first frame update
    void Start()
    {
        playerCam.SetActive(false);
        timelineCam.SetActive(true);
        playerInput.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("DelayCam", 58f);
        //timeToWait++;
        //if (timeToWait >= 2000)
        //{
        //    timelineCam.SetActive(false);
        //    playerCam.SetActive(true);
        //    timeToWait--;
        //}        
    }

    public void DelayCam()
    {
        timelineCam.SetActive(false);
        playerCam.SetActive(true);
        playerInput.SetActive(true);
    }
  
}

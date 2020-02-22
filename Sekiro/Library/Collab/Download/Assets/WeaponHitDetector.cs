using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitDetector : MonoBehaviour
{
    public int numberOfHits = 0;
    public bool isHit;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            numberOfHits += 1;
            isHit = true;
        }
    }
}

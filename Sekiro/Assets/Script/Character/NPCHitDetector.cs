﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHitDetector : MonoBehaviour
{
    public int numberOfHits = 0;
    public bool isHit;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            numberOfHits += 1;
            isHit = true;
            print("Enemy is hit");
            print("Hit is detected");
        }
        
        Invoke("ResetValue", 3f);
    }

    void ResetValue()
    {
        isHit = false;
        numberOfHits = 0;
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHitDetector : MonoBehaviour
{
    public int numberOfHits = 0;
    public bool isHit;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            numberOfHits += 1;
            isHit = true;
        }
        else
        {
            numberOfHits = 0;
            isHit = false;
        }
    }
}

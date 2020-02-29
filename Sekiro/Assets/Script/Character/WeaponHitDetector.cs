using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitDetector : MonoBehaviour
{
    [SerializeField]
    private CharacterCombat combat;
    [SerializeField]
    private CharacterStat targetStat;
    public int numberOfHits = 0;
    public bool isHit;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            targetStat = other.GetComponent<CharacterStat>();
            if (targetStat.alive)
            {
                combat.Attack(targetStat);
                numberOfHits += 1;
                isHit = true;
            }
        }
    }
}

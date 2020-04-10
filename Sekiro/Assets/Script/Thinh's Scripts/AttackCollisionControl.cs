using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollisionControl : MonoBehaviour
{ 

    [SerializeField] private GameObject leftArmAttackCol = null, rightArmAttackCol = null, leftLegAttackCol= null, rightLegAttackCol=null, swordAttackCol=null;

    CharacterAudio char_audio;
    private void Start()
    {
        char_audio = GetComponent<CharacterAudio>();
    }
    public void ActivateLeftArmCollision()
    {
        char_audio.NormalAttackSFX();
        leftArmAttackCol.SetActive(true);
    }

    public void DeactivateLeftArmCollision()
    {
        if (leftArmAttackCol.activeInHierarchy)
        {
            leftArmAttackCol.SetActive(false);
        }
    }

    public void ActivateRightArmCollision()
    {
        char_audio.NormalAttackSFX();
        rightArmAttackCol.SetActive(true);
    }

    public void DeactivateRightArmCollision()
    {
        if (rightArmAttackCol.activeInHierarchy)
        {
            rightArmAttackCol.SetActive(false);
        }
    }

    public void ActivateLeftLegCollision()
    {
        char_audio.NormalAttackSFX();
        leftLegAttackCol.SetActive(true);
    }

    public void DeactivateLeftLegCollision()
    {
        if (leftLegAttackCol.activeInHierarchy)
        {
            leftLegAttackCol.SetActive(false);
        }
    }

    public void ActivateRightLegCollision()
    {
        char_audio.NormalAttackSFX();
        rightLegAttackCol.SetActive(true);
    }

    public void DeactivateRightLegCollision()
    {
        if (rightLegAttackCol.activeInHierarchy)
        {
            rightLegAttackCol.SetActive(false);
        }
    }

    public void ActivateSwordAttackCollision()
    {
        char_audio.SwordAttackSFX();
        swordAttackCol.SetActive(true);
    }

    public void DeactivateSwordAttackCollision()
    {
        if (swordAttackCol.activeInHierarchy)
        {
            swordAttackCol.SetActive(false);
        }
    }
}

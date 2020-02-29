using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollisionControl : MonoBehaviour
{

    [SerializeField] private GameObject leftArmAttackCol = null, rightArmAttackCol = null, leftLegAttackCol= null, rightLegAttackCol=null, swordAttackCol=null;

    public void ActivateLeftArmCollision()
    {
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

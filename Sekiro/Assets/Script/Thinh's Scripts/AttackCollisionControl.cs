using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollisionControl : MonoBehaviour
{

    [SerializeField] private GameObject leftArmAttackCol, rightArmAttackCol, leftLegAttackCol, rightLegAttackCol;
    [SerializeField] private GameObject[] swordAttackCols = null;

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
        foreach(GameObject col in swordAttackCols)
        {
            col.SetActive(true);
        }
    }

    public void DeactivateSwordAttackCollision()
    {
        foreach(GameObject col in swordAttackCols)
        {
            if (col.activeInHierarchy)
            {
                col.SetActive(false);
            }
        }
    }
}

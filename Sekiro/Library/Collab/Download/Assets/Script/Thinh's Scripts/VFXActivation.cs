using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXActivation : MonoBehaviour
{
    [SerializeField] private GameObject healVFX = null;
    [SerializeField] private GameObject swordTrail=null;
    
    public void HealVFXOn()
    {
        healVFX.SetActive(true);
    }

    public void HealVFXOff()
    {
        if (healVFX.activeInHierarchy)
        {
            healVFX.SetActive(false);
        }
    }

    public void SwordTrailOn()
    {
        swordTrail.SetActive(true);
    }

    public void SwordTrailOff()
    {
        if (swordTrail.activeInHierarchy)
        {
            swordTrail.SetActive(false);
        }
    }
}

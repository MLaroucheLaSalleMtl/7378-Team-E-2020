using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXActivation : MonoBehaviour
{
    [SerializeField] private GameObject tornadoShield=null;
    [SerializeField] private GameObject swordTrail=null;
    
    public void TornadoShieldOn()
    {
        tornadoShield.SetActive(true);
    }

    public void TornadoShieldOff()
    {
        if (tornadoShield.activeInHierarchy)
        {
            tornadoShield.SetActive(false);
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

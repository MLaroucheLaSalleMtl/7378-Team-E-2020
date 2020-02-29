using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXActivation : MonoBehaviour
{
    [SerializeField] private GameObject healVFX = null;
    [SerializeField] private GameObject swordTrail=null;
    [SerializeField] private GameObject tornadoVFX = null;
    private Vector3 offset = new Vector3(0, 1, 1.5f);
    
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

    public void InvokeTornado()
    {
        Destroy(Instantiate(tornadoVFX, transform.position + offset, transform.rotation) as GameObject,3f);
    }
}

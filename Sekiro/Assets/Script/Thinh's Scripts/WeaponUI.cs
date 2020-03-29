using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private PlayerController playerState = null;
    [SerializeField] private Sprite fistIcon = null;
    [SerializeField] private Sprite kunaiIcon = null;
    [SerializeField] private Sprite shurikenIcon = null;
    [SerializeField] private Sprite katanaIcon = null;
    [SerializeField] private Image weaponImage = null;
    [SerializeField] private GameObject kunaiInHand = null;
    [SerializeField] private GameObject shurikenInHand = null;

    // Start is called before the first frame update
    void Start()
    {
        weaponImage.sprite = fistIcon;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerState.GetCurrentPlayerState() == State.Unarmed)
        {
            if (kunaiInHand.activeInHierarchy)
            {
                weaponImage.sprite = kunaiIcon;
            }
            else if (shurikenInHand.activeInHierarchy)
            {
                weaponImage.sprite = shurikenIcon;
            }
            else
            {
                weaponImage.sprite = fistIcon;
            }
        }
        else if(playerState.GetCurrentPlayerState() == State.SwordAttaching)
        {
            if (kunaiInHand.activeInHierarchy)
            {
                weaponImage.sprite = kunaiIcon;
            }
            else if (shurikenInHand.activeInHierarchy)
            {
                weaponImage.sprite = shurikenIcon;
            }
            else
            {
                weaponImage.sprite = katanaIcon;
            }
        }
    }
}

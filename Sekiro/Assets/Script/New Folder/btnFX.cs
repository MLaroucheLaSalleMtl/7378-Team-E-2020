using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnFX : MonoBehaviour
{
    public AudioSource myFX;
    public AudioClip hoverFX;
    public AudioClip clickFX;

    public void HoverSound()
    {
        myFX.PlayOneShot(hoverFX);
    }
    public void ClickSound()
    {
        Time.timeScale = 1f;
        AudioSource.PlayClipAtPoint(clickFX, Camera.main.transform.position, 1f);
        //myFX.PlayOneShot(clickFX);
    }
}

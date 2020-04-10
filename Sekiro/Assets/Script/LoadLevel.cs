using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Gia Khanh, Le

//Loading scene
public class LoadLevel : MonoBehaviour
{
    //public GameObject loadingWindow; //Refered to the loading screen
    //public Image loadingImage; //Refered to the loading image
    public void Load()
    {
        StartCoroutine(LoadNewScene()); //Load the scene
    }

    IEnumerator LoadNewScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1); 

        operation.allowSceneActivation = false;

        while(!operation.isDone) //Run until the process is done.
        {
            if(operation.progress >= .9f && !operation.allowSceneActivation)
            {
                operation.allowSceneActivation = true;
            }
            float progress = operation.progress; //Progess is the float that goes from 0 to 1 that indicates the current states of the process.
                                                                      //While the operation is running, continuingly update the UI to reflect this variable.
            yield return null; //Wait for a frame
        }
    }

    //private void Update()
    //{
    //    loadingWindow.SetActive(true);
    //}
}

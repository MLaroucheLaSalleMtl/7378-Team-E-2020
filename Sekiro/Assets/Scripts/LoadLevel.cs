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
    
    public GameObject loadingWindow; //Refered to the loading screen
    public Slider loadingBar; //Refered to the loading bar

    public void Load(int sceneToLoad)
    {
        StartCoroutine(LoadNewScene(sceneToLoad)); //Load the scene
    }

    IEnumerator LoadNewScene(int sceneToLoad)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad); //Load the scene asynchronously in the background. 
                                                                             //Keep all the behaviours in the running while loading a new scene.
                                                                             //AsyncOperation is an object that keep tracks on how the operation is going.
        loadingWindow.SetActive(true); //The loading window appears 

        while(!operation.isDone) //Run until the process is done.
        {
            float progress = operation.progress; //Progess is the float that goes from 0 to 1 that indicates the current states of the process.
                                                                      //While the operation is running, continuingly update the UI to reflect this variable.
            loadingBar.value = progress; //The loadingBar's value = progess.
            yield return null; //Wait for a frame
        }
    }
}

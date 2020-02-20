using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private Slider slider;
    [SerializeField] private Text progressTxt;

    public void LoadLevel(int sceneIndex)
        => StartCoroutine(LoadAsynchronously(sceneIndex));

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        if(mainMenu != null)
            mainMenu.SetActive(false);

        loadingScreen.SetActive(true);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            progressTxt.text = progress * 100f + "%";

            yield return null;
        }
    }
}

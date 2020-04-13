﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//(2) Create an enum for different menu windows
public enum MenuWindows { Play, MainMenu, Settings, Null }
public class MenuController : MonoBehaviour
{
    //Gia Khanh, Le
    //Menu-State Machine
    //Main Menu, Settings and Pause

    public GameObject fadeImage;
    public Animator fadeAnim;
    public GameObject loadingWindow;

    //(1)Create game objects that are refered to 3 windows

    [SerializeField] private GameObject MainMenuWindow;
    [SerializeField] private GameObject SetingsWindow;

    public MenuWindows currentWindow; // Get the current window
    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            currentWindow = MenuWindows.MainMenu;
        else
            currentWindow = MenuWindows.Play;
        Invoke("DeactivateFade", 2);
    }
    public void DeactivateFade() =>
        fadeImage.SetActive(false);
    public void Activate() =>
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    private void Update()
    {
        //(3) State machine

        switch (currentWindow)
        {
            case MenuWindows.MainMenu:
                currentWindow = MenuWindows.MainMenu;
                MainMenuWindow.SetActive(true);
                SetingsWindow.SetActive(false);
                break;

            case MenuWindows.Play:
                currentWindow = MenuWindows.Play;
                SetingsWindow.SetActive(false);
                MainMenuWindow.SetActive(false);
                Time.timeScale = 1f; //Unfreeze the time.
                break;

            case MenuWindows.Settings:
                currentWindow = MenuWindows.Settings;
                SetingsWindow.SetActive(true);
                MainMenuWindow.SetActive(false);
                break;
            case MenuWindows.Null:
                SetingsWindow.SetActive(false);
                MainMenuWindow.SetActive(false);
                break;
        }
    }
    //(5) Create functions for the buttons
    public void Begin() =>
        Invoke("DelayButtonBegin", 1.3f);
    public void Setting() =>
        Invoke("Delay", .3f);
    public void Exit() =>
        Invoke("DelayButtonExit", .3f);
    public void Back() =>
        Invoke("DelayButtonBack", .3f);
    public void Delay() =>
        currentWindow = MenuWindows.Settings;
    public void DelayButtonBack() =>
        currentWindow = MenuWindows.MainMenu;
    public void DelayButtonBegin()
    {
        Invoke("Activate", 5f);
        loadingWindow.SetActive(true);
        fadeImage.SetActive(true);
        fadeAnim.SetTrigger("isFade");
        currentWindow = MenuWindows.Null;
    }
    public void DelayButtonExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
    }
}
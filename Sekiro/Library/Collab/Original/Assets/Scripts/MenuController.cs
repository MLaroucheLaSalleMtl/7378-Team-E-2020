using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : BaseSingleton<MenuController>
{
    //Gia Khanh, Le
    //Menu-State Machine
    //Main Menu, Settings and Pause
    //(1)Create game objects that are refered to 3 windows
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject Settings;
    [SerializeField] GameObject Pause;
    //(2) Create an enum for different menu windows
    enum MenuWindows { Play, MainMenu, Pause, Settings }
    MenuWindows currentWindow; // Get the current windown
    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            currentWindow = MenuWindows.MainMenu;
        }
        else
        {
            currentWindow = MenuWindows.Play;
        }
    }
    private void Update()
    {
        //(4) Detect if pause has been pressed or not and if user are playing or pausing.
        if(Input.GetKeyDown(KeyCode.Escape) && currentWindow == MenuWindows.Play) //If playing, pause the game when pressed esc.
        {   
            currentWindow = MenuWindows.Pause;           
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && currentWindow == MenuWindows.Pause) //If pausing, resume the game when pressed esc.
        {
            currentWindow = MenuWindows.Play;           
        }
        //(3) State machine

        switch (currentWindow) 
        {
            case MenuWindows.MainMenu:
                currentWindow = MenuWindows.MainMenu;
                MainMenu.SetActive(true); //Enable only this window, hide others windows.
                Settings.SetActive(false);
                Pause.SetActive(false);
                break;

            case MenuWindows.Play: //If the user is playing, set all windows to inactive
                currentWindow = MenuWindows.Play;
                MainMenu.SetActive(false); 
                Settings.SetActive(false);
                Pause.SetActive(false);
                Time.timeScale = 1f; //Unfreeze the time.
                break;

            case MenuWindows.Pause: //If the user pause the game, freeze the time and enable only PauseWindow.
                Pause.SetActive(true);//Enable only this window, hide others windows.
                MainMenu.SetActive(false);
                Settings.SetActive(false);
                Time.timeScale = 0; //Freeze the time.
                Debug.Log("Pause");
                break;

            case MenuWindows.Settings: //If the user is in Settings, disable everything except for the SettingWindow.
                Settings.SetActive(true);//Enable only this window, hide others windows.
                MainMenu.SetActive(false);
                Pause.SetActive(false);
                Time.timeScale = 0; //Freeze the time.
                break;
        }
    }
    //(5) Create functions for the buttons
    public void Begin()
    {
        //SceneManager.LoadScene(1); //Load level 1
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Load the current scene + 1
    }
    public void Setting()
    {
        currentWindow = MenuWindows.Settings;
        Debug.Log("Setting");
    }

    public void Exit()
    {
        Application.Quit();
    }
    public void Back()
    {
        currentWindow = MenuWindows.MainMenu;
    }
    public void Resume()
    {
        SceneManager.LoadScene(1);
    }
}

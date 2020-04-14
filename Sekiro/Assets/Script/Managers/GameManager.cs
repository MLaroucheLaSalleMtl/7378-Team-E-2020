using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : BaseSingleton<GameManager>
{
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private PlayerStat character;
    [SerializeField] public GameObject PauseWindow;
    [SerializeField] private GameObject VictoryWindow;
    [SerializeField] private GameObject DeathWindow;
    [SerializeField] private GameObject SettingWindow;
    [SerializeField] private GameObject inventoryPanel = null;
    [SerializeField] private GameObject playerInput = null;
    [SerializeField] private GameObject buttonRetry;
    [SerializeField] private GameObject buttonExit;

    private bool isOpeningInventory = true;
    private bool isPausing = false;

    public void OnOpenInventory(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (isOpeningInventory)
            {
                inventoryPanel.SetActive(false);
                isOpeningInventory = false;
            }
            else
            {
                inventoryPanel.SetActive(true);
                isOpeningInventory = true;
            }
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (isPausing)
            {
                Resume();
                isPausing = false;
            }
            else
            {
                Pause();
                isPausing = true;
            }
        }
    }
    enum InGameWindows { Null, Pause, Victory, Death, Settings }
    InGameWindows currentWindow;
    // Start is called before the first frame update
    private void Start()
    {
        buttonRetry.SetActive(false);
        buttonExit.SetActive(false);
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStat>() as PlayerStat;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        switch (currentWindow)
        {
            case InGameWindows.Pause:
                cameraFollow.enabled = false;
                PauseWindow.SetActive(true);
                VictoryWindow.SetActive(false);
                DeathWindow.SetActive(false);
                SettingWindow.SetActive(false);
                Time.timeScale = 0f;
                break;
            case InGameWindows.Death:
                cameraFollow.enabled = false;
                DeathWindow.SetActive(true);
                PauseWindow.SetActive(false);
                VictoryWindow.SetActive(false);
                SettingWindow.SetActive(false);
                Invoke("DelayActive", 7f);
                Time.timeScale = 1f;
                break;
            case InGameWindows.Victory:
                cameraFollow.enabled = false;
                VictoryWindow.SetActive(true);
                DeathWindow.SetActive(false);
                PauseWindow.SetActive(false);
                SettingWindow.SetActive(false);
                Invoke("DelayActive", 7f);
                Time.timeScale = 1f;
                break;
            case InGameWindows.Settings:
                SettingWindow.SetActive(true);
                VictoryWindow.SetActive(false);
                PauseWindow.SetActive(false);
                DeathWindow.SetActive(false);
                Time.timeScale = 0f;
                break;
            case InGameWindows.Null:
                SettingWindow.SetActive(false);
                VictoryWindow.SetActive(false);
                PauseWindow.SetActive(false);
                DeathWindow.SetActive(false);
                Time.timeScale = 1f;
                break;
        }
        if (!enemyManager.BossIsAlive())
            Invoke("WaitForWin", 5f);

        if (character.playerLife < 1)
            Invoke("WaitForDie", 5f);
    }
    public void Resume()
    {
        currentWindow = InGameWindows.Null;
        cameraFollow.enabled = true;
        PauseWindow.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void Pause()
    {
        currentWindow = InGameWindows.Pause;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Retry()
    {
        currentWindow = InGameWindows.Null;
        Invoke("DelayButtonRetry", .3f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
    public void Settings() =>
        currentWindow = InGameWindows.Settings;

public void Back() => currentWindow = InGameWindows.Pause;

    public void WaitForDie()
    {
        currentWindow = InGameWindows.Death;
        Cursor.lockState = CursorLockMode.None;
    }
    public void WaitForWin()
    {
        currentWindow = InGameWindows.Victory;
        Cursor.lockState = CursorLockMode.None;
    }
    public void DelayActive()
    {
        buttonRetry.SetActive(true);
        buttonExit.SetActive(true);
    }
}

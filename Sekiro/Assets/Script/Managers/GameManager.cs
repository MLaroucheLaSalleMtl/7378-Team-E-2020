using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseSingleton<GameManager>
{
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] CharacterStat character;
    // [SerializeField] private GameObject pauseMenu;
    // [SerializeField] private GameObject win;
    // [SerializeField] private GameObject lose;

    // Start is called before the first frame update
    private void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStat>();
        Cursor.lockState = CursorLockMode.Locked;
        //win.SetActive(false);
    }

    private void Update()
    {
        // if (Input.GetButtonUp("Cancel")) //If we press "ESC"
        // {
        //     //isPaused = true;
        //     Time.timeScale = 0f;
        //     //pauseMenu.SetActive(true);
        //     Cursor.lockState = CursorLockMode.None;
        // }

        // if (!enemyManager.BossIsAlive())
        // {
        //     //win.SetActive(true);
        //     Cursor.lockState = CursorLockMode.None;
        //     Time.timeScale = 0f;
        // }

        // if (!character.alive)
        // {
        //     //lose.SetActive(true);
        //     Cursor.lockState = CursorLockMode.None;
        //     Time.timeScale = 0f;
        // }
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        //pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}

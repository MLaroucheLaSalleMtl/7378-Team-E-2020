using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : BaseSingleton<GameManager>
{
    public static GameManager instance = null;

    [SerializeField] EnemyManager enemyManager;
    [SerializeField] CharacterStat character;
    // [SerializeField] private GameObject pauseMenu;
    // [SerializeField] private GameObject win;
    // [SerializeField] private GameObject lose;
    [SerializeField] private GameObject inventoryPanel = null;
    [SerializeField] private GameObject playerInput = null;
    private bool isOpeningInventory = false;


    public void OnOpenInventory(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (isOpeningInventory)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                inventoryPanel.SetActive(false);
                isOpeningInventory = false;
                playerInput.SetActive(true);

            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                inventoryPanel.SetActive(true);
                isOpeningInventory = true;
                playerInput.SetActive(false);
            }
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStat>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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

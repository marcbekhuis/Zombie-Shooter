using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuToggle : MonoBehaviour
{
    // Game paused bool to prevent anything to happen when your in the pause menu.
    public static bool gamePaused;

    [SerializeField]
    private GameObject pauseMenu;

    private void Start()
    {
        gamePaused = false;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuToggle();
        }
    }

    // Switches the the pause menu on and off, also changes any needed variables.
    public void MenuToggle()
    {
        if (!PlayerHealth.gameOver)
        {
            if (pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(false);
                gamePaused = false;
                Cursor.visible = false;
            }
            else
            {
                pauseMenu.SetActive(true);
                gamePaused = true;
                Cursor.visible = true;
            }
        }
    }
}

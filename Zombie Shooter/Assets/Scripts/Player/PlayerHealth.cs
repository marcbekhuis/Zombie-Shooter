using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // Game over bool to prevent anything to happen when your dead.
    public static bool gameOver = false;

    // All the heart icons
    [SerializeField]
    private Image[] hearts;

    [SerializeField]
    private GameObject gameOverMenu;

    private int health = 3;

    private void Start()
    {
        gameOver = false;
    }

    // Lowers the players health with the given amount.
    public void LoseHealth(int lostHealth)
    {
        health = Mathf.Clamp(health - lostHealth, 0, 100);
        if (health == 0)
        {
            gameOver = true;
            gameOverMenu.SetActive(true);
        }
        // changes heart icon to black
        hearts[health].color = new Color(0, 0, 0);
    }
}

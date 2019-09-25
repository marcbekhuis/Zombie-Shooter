using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField]
    private Image[] hearts;

    private int health = 3;

    public void LoseHealth(int lostHealth)
    {
        health = Mathf.Clamp(health - lostHealth, 0, 100);
        if (health == 0)
        {
            Debug.Log("GAME OVER");
        }
        hearts[health].color = new Color(0, 0, 0);
    }
}

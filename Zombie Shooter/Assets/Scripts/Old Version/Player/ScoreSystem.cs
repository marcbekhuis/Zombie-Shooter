using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    // Ingame score system does not save the scores for a high score system right now.
    // High score system will probably be added later.
    [SerializeField]
    private Text scoreText;

    private int score;

    private void Start()
    {
        UpdateScoreText();
    }

    public void Addscore(int scoreToAdd)
    {
        score += scoreToAdd;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}

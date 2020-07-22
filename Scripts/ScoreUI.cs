using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Controls text displaying current player score.
/// </summary>
public class ScoreUI : MonoBehaviour
{
    public TMP_Text scoreText;

    private void Awake()
    {
        FindObjectOfType<PlayerStats>().OnPlayerScoreChanged += HandleScoreChange;
    }

    void HandleScoreChange(int score)
    {
        scoreText.text = "Score: " + score;
    }
}

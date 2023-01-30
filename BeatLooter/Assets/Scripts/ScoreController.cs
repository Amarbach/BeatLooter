using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score = 0;
    public int Score { get { return score; } }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = score + "";
    }
}

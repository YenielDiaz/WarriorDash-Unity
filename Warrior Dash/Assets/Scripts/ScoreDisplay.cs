using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    //cached references
    TextMeshProUGUI scoreText;
    [SerializeField] GameSession gameSession;

    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();
    }

    // make the score text equal the current score
    void Update()
    {
        scoreText.SetText(gameSession.GetScore().ToString());
    }
}

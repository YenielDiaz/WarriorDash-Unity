using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int score = 0;
    [SerializeField] TextMeshProUGUI successText;
    [SerializeField] string SuccessMessage = "Success!";

    //on Awake Set up the singleton object
    void Awake()
    {
        SetupSingleton();
    }

    //make sure object persists through the levels, and if there are more than one, destroy this one
    private void SetupSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }

    //method to show success text
    public void showSuccess()
    {
        successText.text = SuccessMessage;
    }

    //get score
    public int GetScore() { return score; }

    //increase score
    public void AddToScore(int scoreToAdd) { score += scoreToAdd; }

    //reset the score by destroying this gameObject
    public void ResetGame() { Destroy(gameObject); }
}

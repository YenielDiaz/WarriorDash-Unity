using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    //config parameters
    [SerializeField] private float delay = 3f;

    //calls a coroutine to load the game over scene after a few seconds of delay
    public void LoadGameOver()
    {
        StartCoroutine(DelayAndLoadGameOver());
    }

    //load the first playable game scene
    public void LoadGameScene()
    {
        FindObjectOfType<GameSession>().ResetGame();
        SceneManager.LoadScene("Game");
    }

    //load the next scene
    public void LoadNextLevel()
    {
        StartCoroutine(DelayAndLoadNextScene());
    }

    //load the start menu scene
    public void LoadStartMenu() { SceneManager.LoadScene("Start Menu"); }

    //quit the game
    public void QuitGame() { Application.Quit(); }

    //delay for a few seconds then load the game over scene
    private IEnumerator DelayAndLoadGameOver()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Game Over");
    }

    //coroutine that loads next level after a delay
    private IEnumerator DelayAndLoadNextScene()
    {
        //FindObjectOfType<GameSession>().showSuccess(); //show success if player survived the level
        int currSceneIndex = SceneManager.GetActiveScene().buildIndex; //getting the index of the current scene
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(currSceneIndex + 1); //loading next scene
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Sprite[] lives;

    public Image livesImageDisplay;

    public Text scoreText;

    public Image mainMenu;

    public int score;


    public void UpdateLives(int currentLives)
    {
        livesImageDisplay.sprite = lives[currentLives];
    }

    public void UpdateScore()
    {
        score += 10;

        scoreText.text = "Score: " + score;
    }

    public void InitialScore()
    {
        scoreText.text = "Score: 0";
    }

    public void ShowMainMenu()
    {
        StartCoroutine(WaitToDisplayCoroutine());
    }

    IEnumerator WaitToDisplayCoroutine()
    {
        yield return new WaitForSecondsRealtime(3);
        SceneManager.LoadScene("ContinuePrompt");
    }

    public void ResetCounter()
    {
        scoreText.text = "Score: 0";
    }

    public void ResumeGame()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.ResumeGame();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Home");
    }
}

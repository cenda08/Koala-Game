using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class LogicScript : MonoBehaviour
{

    public int SleepScore;
    public Text SleepScoreText;
    public Text FinalSleepScoreText;
    public GameObject GameOverScreen;
    public GameObject Hint;
    public GameObject HintScreen;
    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;

    [ContextMenu ("Decrease Sleep Score")]
    public void ScoreDecrease(int amount) {

        SleepScore -= amount;
        SleepScoreText.text = SleepScore.ToString();

        /*if (SleepScore <= 0)
        {
            GameOver();
        }
        */
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Game Restarted");

    }

    public void GameOver()
    {
        GameOverScreen.SetActive(true);
        Hint.SetActive(false);
        FinalSleepScoreText.text = SleepScore.ToString();
        SleepScoreText.gameObject.SetActive(false);
        HintScreen.SetActive(false);
        StarsCount();
    }


    public void StarsCount()
    {
        if (0 < SleepScore && SleepScore <= 35)
        {
            Star1.SetActive(true);
        }

        else if (25 < SleepScore && SleepScore <= 74)
        {
            Star1.SetActive(true);
            Star2.SetActive(true);
        }

        else if (75 <= SleepScore && SleepScore <= 100)
        {
            Star1.SetActive(true);
            Star2.SetActive(true);
            Star3.SetActive(true);
        }
    }
}
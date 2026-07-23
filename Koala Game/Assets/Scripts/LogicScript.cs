using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class LogicScript : MonoBehaviour
{

    private int SleepScore = 100;
    public Text SleepScoreText;
    public Text FinalSleepScoreText;
    public GameObject GameOverScreen;

    [ContextMenu ("Decrease Sleep Score")]
    public void ScoreDecrease() {

        SleepScore -= 20;
        SleepScoreText.text = SleepScore.ToString();
    
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Game Restarted");

    }

    public void GameOver()
    {
        GameOverScreen.SetActive(true);

    }

}

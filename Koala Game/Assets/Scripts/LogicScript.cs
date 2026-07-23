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

    [ContextMenu ("Decrease Sleep Score")]
    public void ScoreDecrease(int amount) {

        SleepScore -= amount;
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
        Hint.SetActive(false);
        SleepScoreText.gameObject.SetActive(false);
    }

}

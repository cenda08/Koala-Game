using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using UnityEditor.PackageManager;
using System.Linq;
public class LogicScript : MonoBehaviour
{

    public int SleepScore;
    public int CurrentPhase = 1;
    public Text SleepScoreText;
    public Text TimerText;
    public Text FinalSleepScoreText;
    public GameObject GameOverScreen;
    public GameObject GunIcon;
    public GameObject Hint;
    public GameObject HintScreen;
    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;
    public CarScript car;
    public BirdScript bird;
    public List<string> eventCycle = new List<string>();
    public List<float> eventCooldowns = new List<float>();
    public int TotalEvents = 10; // Celkový počet eventů

    [ContextMenu ("Randomize all Events")]
    public void RandomizeEvents()
    {
        string[] events = {"Bird", "Snake", "Phone", "Phone", "Bird", "Car"}; // Array, ze kterého beru názvy eventů na random generování cycklu - CaseSensitive
        for(int i=0; i<TotalEvents; i++)
        {
            int x = UnityEngine.Random.Range(0,events.Length);
            float randomCooldown = UnityEngine.Random.Range(0,10); // Cooldown mezi eventy
            eventCycle.Add(events[x]);
            eventCooldowns.Add(randomCooldown);
        }
        eventCooldowns[0] = 3; // Cooldown od prvního eventu
        
        foreach(String i in eventCycle)
        {
            Debug.Log(i);
        }
    }

    void Start()
    {
        
        string difficulty = ButtonController.ChosenDifficulty;
        if (difficulty == "Easy")
        {
            TotalEvents = 10;
            car.CarSpeed = 0.45f;
            bird.BirdSpeed = 1f;
            Hint.SetActive(true);
        }
        else if (difficulty == "Hard")
        {
            TotalEvents = 15;
            car.CarSpeed = 0.8f;
            bird.BirdSpeed = 1.5f;
            Hint.SetActive(false);
        }


        RandomizeEvents();
        TimerText.text = CurrentPhase.ToString() + " / " + TotalEvents;
    }
    void Update()
    {
        if(eventCycle.Count == 0)
        {
            new WaitForSeconds(10);
            GameOver();
        }
        if(SleepScore == 0)
        {
            GameOver();        }
    }

    [ContextMenu ("Decrease Sleep Score")]
    public void ScoreDecrease(int amount) {

        SleepScore -= amount;
        SleepScoreText.text = SleepScore.ToString();

    }

    [ContextMenu ("Restart Game")]
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Game Restarted");

    }

    [ContextMenu ("End Game")]

    public void GameOver()
    {
        StartCoroutine(GameOverSequence());
    }

    IEnumerator GameOverSequence()
    { 
        GameOverScreen.SetActive(true);
        Hint.SetActive(false);
        GunIcon.SetActive(false);
        FinalSleepScoreText.text = SleepScore.ToString();
        SleepScoreText.gameObject.SetActive(false);
        HintScreen.SetActive(false);
        StarsCount();
        yield return null;
        // tady se zobrazí Game Over screen
    }
  




    public void StarsCount()
    {
        if (0 < SleepScore && SleepScore <= 35)
        {
            Star1.SetActive(true);
        }

        else if (35 < SleepScore && SleepScore <= 74)
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
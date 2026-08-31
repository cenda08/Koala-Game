using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    bool isPaused = false;
    public GameObject PauseMenu;
   
  
    void OnMouseDown()
    {
        if (!isPaused)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }

    public void Pause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }


    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void Quit()
{
    Time.timeScale = 1f;
    SceneManager.LoadScene("Main Menu");
}


}
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private string NewGame = "Game";
    public static string ChosenDifficulty = "none";
    public void NewGameButton()
    {
        SceneManager.LoadScene(NewGame);
    }
    
    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void EasyMode()
    {
        ChosenDifficulty = "Easy";
        NewGameButton();
    }
    public void HardMode()
    {
        ChosenDifficulty = "Hard";
        NewGameButton();
    }
}

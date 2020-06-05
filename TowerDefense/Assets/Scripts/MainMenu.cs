using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string optionsSceneName = "Options";
    public string levelSelectSceneName = "LevelSelect";
    public string endlessGameSceneName = "EndlessMode";

    public void Play()
    {
        SceneFader.Instance.FadeTo(levelSelectSceneName, LoadType.Menu);
    }

    public void Quit()
    {
        Debug.Log("Quiting the game...");
        Application.Quit();
    }

    public void Options()
    {
        Debug.Log("Loading options menu.");
        SceneFader.Instance.FadeTo(optionsSceneName, LoadType.Menu);
    }

    public void Highscores()
    {
        //TODO
        Debug.Log("Display highscores.");
    }

    public void Credits()
    {
        //TODO
        Debug.Log("Display credits.");
    }

    public void PlayEndlessMode()
    {
        //TODO
        Debug.Log("Playing endless game.");
    }

    public void PlaySound(string s)
    {
        AudioManager.Instance.Play(s);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public SceneFader sceneFader;

    public void Play(string levelToLoad)
    {
        if (levelToLoad == null) levelToLoad = "LevelSelect";
        sceneFader.FadeTo(levelToLoad);
    }

    public void Quit()
    {
        Debug.Log("Quiting the game...");
        Application.Quit();
    }

    public void Options()
    {
        //TODO
        Debug.Log("Loading options menu.");
    }

    public void WallOfGlory()
    {
        //TODO
        Debug.Log("Loading highscores scene.");
    }

    public void Credits()
    {
        //TODO
        Debug.Log("Loading credits scene.");
    }
}

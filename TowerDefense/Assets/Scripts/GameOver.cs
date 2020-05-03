using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public string menuSceneName = "MainMenu";
    public SceneFader sceneFader = SceneFader.Instance;

    public void Retry()
    {
        sceneFader.ReloadLevel();
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName, LoadType.Menu);
    }
}

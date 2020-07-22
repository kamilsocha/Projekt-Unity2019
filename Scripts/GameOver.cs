using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls game over panel.
/// </summary>
public class GameOver : MonoBehaviour
{
    public string menuSceneName = "MainMenu";
    public string gameOverSound = "GameOver";

    public void Retry()
    {
        SceneFader.Instance.ReloadLevel();
    }

    public void Menu()
    {
        SceneFader.Instance.FadeTo(menuSceneName, LoadType.Menu);
    }

    private void OnEnable()
    {
        AudioManager.Instance.Play(gameOverSound);
    }
}

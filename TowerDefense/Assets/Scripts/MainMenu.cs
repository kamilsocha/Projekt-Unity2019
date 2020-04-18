using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play(string levelToLoad)
    {
        if (levelToLoad == null) levelToLoad = "SelectLevel";
        SceneManager.LoadScene(levelToLoad);
    }
    public void Quit()
    {
        Debug.Log("Quiting the game...");
        Application.Quit();
    }
}

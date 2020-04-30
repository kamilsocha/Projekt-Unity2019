using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    public string menuName = "MainMenu";
    public string nextLevel = "Level02";
    public int levelToUnlock = 2;
    public SceneFader sceneFader;

    private void Awake()
    {
        if (levelToUnlock > PlayerPrefs.GetInt("levelReached", 1))
        {
            PlayerPrefs.SetInt("levelReached", levelToUnlock);
        }
    }

    public void Continue()
    {
        sceneFader.FadeTo(nextLevel);
    }

    public void BackToMenu()
    {
        sceneFader.FadeTo(menuName);
    }
}

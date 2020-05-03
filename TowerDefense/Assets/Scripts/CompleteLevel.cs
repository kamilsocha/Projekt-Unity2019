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

        sceneFader.FadeTo(nextLevel, LoadType.SingleLevel);
    }

    public void BackToMenu()
    {
        sceneFader.FadeTo(menuName, LoadType.Menu);
    }

    private void OnEnable()
    {
        //TODO check if there is next level
        //if()
    }
}

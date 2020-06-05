using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    public string menuName = "MainMenu";
    public string nextLevel = "Level01";
    public int levelToUnlock = 2;

    public delegate void ContinueButtonEvent();
    public event ContinueButtonEvent OnContinue;

    private void Awake()
    {
        if (levelToUnlock > PlayerPrefs.GetInt("levelReached", 1))
        {
            PlayerPrefs.SetInt("levelReached", levelToUnlock);
        }
    }

    public void SetNextLevel(string _nextLevel, int _levelToUnlock)
    {
        nextLevel = _nextLevel;
        levelToUnlock = _levelToUnlock;
    }

    public void SetLevelToUnlock(int _levelToUnlock)
    {
        levelToUnlock = _levelToUnlock;
    }

    public void Continue()
    {
        OnContinue?.Invoke();
        SceneFader.Instance.FadeTo(nextLevel, LoadType.SingleLevel);
        gameObject.SetActive(false);
    }

    public void BackToMenu()
    {
        SceneFader.Instance.FadeTo(menuName, LoadType.Menu);
    }

}

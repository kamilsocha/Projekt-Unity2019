using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Button[] levelButtons;

    static string menuName = "MainMenu";
    int defaultLevel = 7;

    private void Start()
    {
        /// <summary>
        /// Block levels which the player has not unlocked yet.
        /// </summary>
        int levelReached = PlayerPrefs.GetInt("levelReached", defaultLevel);
        for (int i = levelReached; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = false;
        }
    }

    public void Select(string levelName)
    {
        PlayerPrefs.SetString("CurrentLevel", levelName);
        Debug.Log("levelName from button = " + levelName);
        SceneFader.Instance.FadeTo(levelName, LoadType.DoubleLevel);
    }

    public void BackToMainMenu()
    {
        SceneFader.Instance.FadeTo(menuName, LoadType.Menu);
    }
}

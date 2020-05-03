using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public SceneFader sceneFader;
    public Button[] levelButtons;

    static string menuName = "MainMenu";
    static string levelString = "Level";
    //string levelName = levelString;
    //string levelNumber = "1";
    int defaultLevel = 1;

    private void Start()
    {
        sceneFader = FindObjectOfType<SceneFader>();

        /*for (int i = 0; i < levelButtons.Length; i++)
        {
            levelName = levelString;
            if (i < 10) levelName += "0";
            levelNumber = (i + 1).ToString();
            levelName += levelNumber;
            levelButtons[i].GetComponentInChildren<Text>().text = levelNumber;
            levelButtons[i].onClick.AddListener(delegate { Select(levelName); });
            Debug.Log("button " + i + " levelName = " + levelName);
        }*/
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
        sceneFader.FadeTo(levelName, LoadType.DoubleLevel);
    }

    public void BackToMainMenu()
    {
        sceneFader.FadeTo(menuName, LoadType.Menu);
    }
}

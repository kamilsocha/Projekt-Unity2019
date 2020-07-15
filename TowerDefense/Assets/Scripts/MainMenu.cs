using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string settingsSceneName = "Settings";
    public string levelSelectSceneName = "LevelSelect";
    public string endlessGameSceneName = "EndlessMode";

    public GameObject creditsScreen;
    public GameObject highscoreScreen;
    public TMP_Text[] scoreTexts; 
    public PlayerLevelData[] playerLevelDatas;
    Animator animator;
    public string filePath = "playerStats";
    string lockedText = "locked";

    private void Start()
    {
        creditsScreen.SetActive(false);
        animator = GetComponent<Animator>();

        highscoreScreen.SetActive(false);
        SaveData.Current = SerializationManager.Load(filePath) as SaveData;
        playerLevelDatas = SaveData.Current.playerLevelDatas.ToArray();

        int i;
        for(i = 0; i < playerLevelDatas.Length; i++)
        {
            scoreTexts[i].text = playerLevelDatas[i].bestScore.ToString();
        }
        for(int j = i; j < scoreTexts.Length; j++)
        {
            scoreTexts[j].text = lockedText;
        }
    }

    public void Play()
    {
        SceneFader.Instance.FadeTo(levelSelectSceneName, LoadType.Menu);
    }

    public void Quit()
    {
        Debug.Log("Quiting the game...");
        Application.Quit();
    }

    public void Settings()
    {
        Debug.Log("Loading settings menu.");
        SceneFader.Instance.FadeTo(settingsSceneName, LoadType.Menu);
    }

    public void ShowHighscores()
    {
        Debug.Log("Display highscores.");
        highscoreScreen.SetActive(true);
    }

    public void HideHighscores()
    {
        highscoreScreen.GetComponent<Animator>().SetTrigger("FadeOut");
    }

    public void Credits()
    {
        creditsScreen.SetActive(true);
        animator.SetTrigger("Credits");
    }

    public void DeactivateCredits()
    {
        creditsScreen.SetActive(false);
    }

    public void PlaySound(string s)
    {
        AudioManager.Instance.Play(s);
    }

}

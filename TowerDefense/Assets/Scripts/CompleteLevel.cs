using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls complete level screen.
/// </summary>
public class CompleteLevel : MonoBehaviour
{
    public string menuName = "MainMenu";
    public string nextLevel = "Level01";
    public int levelToUnlock = 2;

    public delegate void ContinueButtonEvent();
    public event ContinueButtonEvent OnContinue;

    public Button completeLevelButton;
    public Button menuButton;

    public Button[] buttons;

    public TMP_Text saveText;
    public float letterPause = 0.01f;
    bool isSaved;
    public string savingText = "Saving...";
    public string savedText = "Game Saved";

    private void Awake()
    {
        if (levelToUnlock > PlayerPrefs.GetInt("levelReached", 1))
        {
            PlayerPrefs.SetInt("levelReached", levelToUnlock);
        }
        FindObjectOfType<GameManager>().OnDataSaved += HandleDataSaved;

        buttons = GetComponentsInChildren<Button>();
        foreach (var button in buttons) button.interactable = false;
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

    private void OnEnable()
    {
        isSaved = false;
        StartCoroutine(Saving());
    }

    IEnumerator Saving()
    {
        var length = savingText.Length;
        while(!isSaved)
        {
            for (int i = 0; i < length; i++)
            {
                saveText.text = savingText.Substring(0, i);
                yield return new WaitForSeconds(letterPause);
            }
            yield return new WaitForSeconds(letterPause);
        }
        saveText.text = savedText;
        yield return new WaitForSeconds(3f);
        saveText.enabled = false;
        isSaved = false;
    }

    void HandleDataSaved()
    {
        isSaved = true;
        foreach (var button in buttons) button.interactable = true;
    }
}

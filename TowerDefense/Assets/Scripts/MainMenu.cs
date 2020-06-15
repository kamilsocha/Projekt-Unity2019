using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string settingsSceneName = "Settings";
    public string levelSelectSceneName = "LevelSelect";
    public string endlessGameSceneName = "EndlessMode";

    public GameObject creditsScreen;
    Animator animator;
    float creditsTime;

    private void Start()
    {
        creditsScreen.SetActive(false);
        animator = creditsScreen.GetComponent<Animator>();
        Debug.Log($"animator: {animator.name}");
        RuntimeAnimatorController ac = animator.runtimeAnimatorController;
        foreach (var c in ac.animationClips)
        {
            creditsTime += c.length;
        }
        Debug.Log($"clips length: {creditsTime}");
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

    public void Highscores()
    {
        //TODO
        Debug.Log("Display highscores.");
    }

    public void Credits()
    {
        //TODO
        Debug.Log("Display credits.");
        creditsScreen.SetActive(true);
        StartCoroutine(ShowCredits());
    }

    public void PlayEndlessMode()
    {
        //TODO
        Debug.Log("Playing endless game.");
    }

    public void PlaySound(string s)
    {
        AudioManager.Instance.Play(s);
    }

    IEnumerator ShowCredits()
    {
        yield return new WaitForSeconds(creditsTime - 1f);
        creditsScreen.SetActive(false);
    }

}

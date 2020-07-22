using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages pause menu.
/// </summary>
public class PauseMenu : MonoBehaviour
{
    public GameObject ui;
    public GameObject controlsUI;
    public TMP_Text cameraMovementEnableKey;
    Animator animator;

    public string menuSceneName = "MainMenu";

    private void Awake()
    {
        animator = ui.GetComponent<Animator>();
        cameraMovementEnableKey.text = PlayerPrefs.GetString("cameraMovementEnableKey", "m");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        //if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        if (GameManager.GameIsOver) return;

        ui.SetActive(!ui.activeSelf);
        if(ui.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void Retry()
    {
        SceneFader.Instance.ReloadLevel();
        Toggle();
    }

    public void Menu()
    {
        SceneFader.Instance.FadeTo(menuSceneName, LoadType.Menu);
        Toggle();
    }

    public void Controls()
    {
        if (controlsUI.activeSelf)
            animator.SetTrigger("ControlsIn");
        controlsUI.SetActive(!controlsUI.activeSelf);
    }
}

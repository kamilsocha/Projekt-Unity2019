using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject ui;
    public GameObject controlsUI;
    Animator animator;

    public string menuSceneName = "MainMenu";

    private void Awake()
    {
        animator = ui.GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
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

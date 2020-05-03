using UnityEngine;

public class OptionsController : MonoBehaviour
{
    public SceneFader sceneFader = SceneFader.Instance;
    string menuSceneName = "MainMenu";

    public void BackToMainMenu()
    {
        sceneFader.FadeTo(menuSceneName, LoadType.Menu);
    }
}

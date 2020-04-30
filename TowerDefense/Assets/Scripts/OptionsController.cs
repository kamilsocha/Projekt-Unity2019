using UnityEngine;

public class OptionsController : MonoBehaviour
{
    public SceneFader sceneFader;
    string menuName = "MainMenu";

    public void BackToMainMenu()
    {
        sceneFader.FadeTo(menuName);
    }
}

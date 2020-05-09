using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsController : MonoBehaviour
{
    public SceneFader sceneFader;
    string menuSceneName = "MainMenu";
    Resolution[] resolutions;

    public AudioMixer audioMixer;
    public TMP_Dropdown graphicsDropdown;
    public TMP_Dropdown resolutionDropdown;

    private void Awake()
    {
        sceneFader = FindObjectOfType<SceneFader>();

        graphicsDropdown.SetValueWithoutNotify(QualitySettings.GetQualityLevel());

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> resolutionsList = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string resolutionString = resolutions[i].width + "x" + resolutions[i].height;
            resolutionsList.Add(resolutionString);

            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(resolutionsList);
        resolutionDropdown.SetValueWithoutNotify(currentResolutionIndex);
    }

    public void BackToMainMenu()
    {
        sceneFader.FadeTo(menuSceneName, LoadType.Menu);
    }

    public void SetVolume(float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

}

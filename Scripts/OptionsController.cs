using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Controls options panel with graphics quality, screen resolution and sound settings.
/// </summary>
public class OptionsController : MonoBehaviour
{
    string menuSceneName = "MainMenu";
    Resolution[] resolutions;
    public GameObject settingsUI;


    public AudioMixer themeAudioMixer;
    public AudioMixer soundsAudioMixer;
    public TMP_Dropdown graphicsDropdown;
    public TMP_Dropdown resolutionDropdown;


    public TurretUI turretUI;
    public ControlsUI controlsUI;

    string buttonAudioName = "ButtonClick";

    /// <summary>
    /// Gets all possible resolutions with getting rid of repeating ones.
    /// Sets current quality settings and current resolution.
    /// </summary>
    private void Awake()
    {
        graphicsDropdown.SetValueWithoutNotify(QualitySettings.GetQualityLevel());

        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        resolutionDropdown.ClearOptions();

        List<string> resolutionsList = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string resolutionString = resolutions[i].width + "x" + resolutions[i].height;
            resolutionsList.Add(resolutionString);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(resolutionsList);
        resolutionDropdown.SetValueWithoutNotify(currentResolutionIndex);

        settingsUI.SetActive(true);
    }

    public void BackToMainMenu()
    {
        SceneFader.Instance.FadeTo(menuSceneName, LoadType.Menu);
    }

    public void SetThemeVolume(float volume)
    {
        Debug.Log(volume);
        themeAudioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
    }

    public void SetSoundsVolume(float volume)
    {
        Debug.Log(volume);
        soundsAudioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
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

    public void ShowTurretsPanel()
    {
        turretUI.Show();
    }

    public void ShowControlsPanel()
    {
        controlsUI.Show();
    }

    public void PlaySound(string s)
    {
        if (s == null) s = buttonAudioName;
        AudioManager.Instance.Play(s);
    }

}

using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Contains all sounds and plays them.
/// Placing all sounds in one place makes it easier to manage them.
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public string theme = "Theme";
    public float themeOffset = 5f;
    public Sound[] themes;
    public Sound[] sounds;

    void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("AudioManager gets destroyed.");
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this);

        foreach (Sound t in themes)
        {
            t.source = gameObject.AddComponent<AudioSource>();
            t.source.clip = t.clip;
            t.source.volume = t.volume;
            t.source.pitch = t.pitch;
            t.source.loop = t.loop;
            t.source.outputAudioMixerGroup = t.output;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.output;
        }
    }

    private void Start()
    {
        float totalThemesTime = 0;
        foreach(var theme in themes)
        {
            totalThemesTime += theme.clip.length;
        }
        InvokeRepeating("StartThemes", 0, totalThemesTime + themeOffset);
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning($"Sound {name} wasn't found.");
            return;
        }
        s.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning($"Sound {name} wasn't found.");
            return;
        }
        s.Stop();
    }

    public void PlayTheme(string name)
    {
        Sound t = Array.Find(themes, theme => theme.name == name);
        if (t == null)
        {
            Debug.LogWarning($"Theme {name} wasn't found.");
            return;
        }
        t.Play();
    }

    void StartThemes()
    {
        StartCoroutine(PlayThemes());
    }

    IEnumerator PlayThemes()
    {
        foreach(var theme in themes)
        {
            PlayTheme(theme.name);
            yield return new WaitForSeconds(theme.clip.length);
        }
    }
}

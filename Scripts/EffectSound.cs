using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Plays sound when object is created.
/// Usually used to play looping sounds.
/// </summary>
public class EffectSound : MonoBehaviour
{
    public string effectSoundName = "Death1";

    private void Awake()
    {
        AudioManager.Instance.Play(effectSoundName);
    }
}

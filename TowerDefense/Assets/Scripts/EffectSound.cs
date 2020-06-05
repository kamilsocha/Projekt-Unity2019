using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSound : MonoBehaviour
{
    public string effectSoundName = "Death1";

    private void Awake()
    {
        AudioManager.Instance.Play(effectSoundName);
    }
}

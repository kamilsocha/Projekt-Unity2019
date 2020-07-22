using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Displays text informing whether camera movement is turned on or off.
/// </summary>
public class CameraUI : MonoBehaviour
{
    public string cameraTextToDisplay = "Camera Move: ";

    public GameObject text;
    public TMP_Text cameraText;
    public TMP_Text cameraMovementKeyText;
    
    private void Awake()
    {
        var cameraMovementEnableKey = PlayerPrefs.GetString("cameraMovementEnableKey", "m");
        cameraMovementKeyText.text = "Press " + cameraMovementEnableKey.ToUpper();
        cameraText.text = cameraTextToDisplay +  "OFF";
    }

    public void ToggleCameraText(bool movement)
    {
        if(movement)
        {
            cameraText.text = cameraTextToDisplay + "ON";
        } else
        {
            cameraText.text = cameraTextToDisplay + "OFF";
        }
    }

    public void Hide() { text.SetActive(false); }
    public void Show() { text.SetActive(true);  }
}

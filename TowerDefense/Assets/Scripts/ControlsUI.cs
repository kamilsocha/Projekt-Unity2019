using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlsUI : MonoBehaviour
{
    public GameObject ui;
    Animator animator;
    public TMP_InputField cameraMovementEnableKeyInput;

    private void Awake()
    {
        ui.SetActive(false);
        animator = GetComponent<Animator>();

        var key = PlayerPrefs.GetString("cameraMovementEnableKey", "m");
        cameraMovementEnableKeyInput.text = key;

        cameraMovementEnableKeyInput.onValueChanged.AddListener(delegate
        {
            PlayerPrefs.SetString("cameraMovementEnableKey", cameraMovementEnableKeyInput.text.ToLower());
        });
    }

    public void Show()
    {
        ui.SetActive(true);
        animator.SetTrigger("In");
    }

    public void Hide()
    {
        animator.SetTrigger("Out");
    }

    public void Deactivate() { ui.SetActive(false); }
}

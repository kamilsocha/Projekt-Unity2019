using UnityEngine;

public class TurretPanelUI : MonoBehaviour
{
    public GameObject ui;

    private void Awake()
    {
        ui.SetActive(false);
    }

    public void Show()
    {
        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }
}

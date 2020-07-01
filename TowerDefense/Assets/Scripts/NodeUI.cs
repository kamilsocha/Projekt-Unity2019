using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;
    public TMP_Text upgradeCost;
    public TMP_Text sellAmount;
    public Button upgradeButton;
    public Button turretCameraButton;

    Node target;
    BuildManager buildManager;

    SecondCameraController secondCamera;
    public GameObject turretCameraHint;
    string exitTurretCameraKey = "C";

    public delegate void CamerasSwitchEvent();
    public event CamerasSwitchEvent OnCamerasSwitch;

    private void Awake()
    {
        ui.SetActive(false);
        secondCamera = FindObjectOfType<SecondCameraController>();
        buildManager = BuildManager.Instance;
        turretCameraHint.GetComponentInChildren<TMP_Text>().text =
            $"press {exitTurretCameraKey} to exit turret camera mode";
        turretCameraHint.SetActive(false);
    }

    private void Update()
    {
        if(secondCamera.isActive && Input.GetKeyDown(KeyCode.C))
        {
            ToggleTurretCamera();
        }
    }

    public void SetTarget(Node _target)
    {
        target = _target;
        transform.position = target.GetBuildPosition();
        if(!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.TurretBlueprint.upgradeCost;
            sellAmount.text = "$" + target.TurretBlueprint.GetSellAmount();
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "DONE";
            upgradeButton.interactable = false;
            sellAmount.text = "$" + target.TurretBlueprint.GetUpgradedSellAmount();
        }
        if(secondCamera.target != null)
        {
            turretCameraButton.interactable = false;
        }

        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
        turretCameraButton.interactable = true;
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.Instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.Instance.DeselectNode();
        target.isUpgraded = false;
    }

    public void ToggleTurretCamera()
    {
        if(!secondCamera.isActive)
        {
            secondCamera.SetTarget(target);
            buildManager.DeselectNode();
            turretCameraHint.SetActive(true);
        } else
        {
            turretCameraHint.SetActive(false);
        }
        OnCamerasSwitch?.Invoke();
    }
}

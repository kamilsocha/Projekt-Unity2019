using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Shop : MonoBehaviour
{
    public StatsUI statsUI;
    public GameObject shopUI;
    public TMP_Text toggleButtonText;
    string activeString = "close";
    string inactiveString = "open";
    string clickAudioName = "ButtonClick";

    public TurretBlueprint crossbowTurret;
    public TurretBlueprint cannonTurret;
    public TurretBlueprint laserBeamerTurret;
    public TurretBlueprint laserTurret;
    //TODO
    //public TurretBlueprint antibioticTurret;
    //public TurretBlueprint medicineTurret;

    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.Instance;
        shopUI.SetActive(true);
        var shopUIState = shopUI.activeSelf;
        if (shopUIState)
            toggleButtonText.text = activeString;
        else
            toggleButtonText.text = inactiveString;
    }

    public void SelectCrossbowTurret()
    {
        Debug.Log("Selected crossbow");
        buildManager.SelectTurretToBuild(crossbowTurret);
    }
    public void MouseOverCrossbowTurret()
    {
        ShowStatsUI(crossbowTurret);
    }

    public void SelectCannonTurret()
    {
        buildManager.SelectTurretToBuild(cannonTurret);
    }
    public void MouseOverCannonTurret()
    {
        ShowStatsUI(cannonTurret);
    }

    public void SelectLaserBeamerTurret()
    {
        buildManager.SelectTurretToBuild(laserBeamerTurret);
    }
    public void MouseOverLaserBeamerTurret()
    {
        ShowStatsUI(laserBeamerTurret);
    }

    public void SelectLaserTurret()
    {
        buildManager.SelectTurretToBuild(laserTurret);
    }
    public void MouseOverLaserTurret()
    {
        ShowStatsUI(laserTurret);
    }
    /* 
     * TODO
    public void SelectAntibioticTurret()
    {

    }

    public void SelectMedicineTurret()
    {

    }
    */

    public void ToggleShopPanel()
    {
        AudioManager.Instance.Play(clickAudioName);
        if(shopUI.activeSelf)
        {
            shopUI.SetActive(false);
            toggleButtonText.text = inactiveString;
        } else
        {
            shopUI.SetActive(true);
            toggleButtonText.text = activeString;
        }
    }

    void ShowStatsUI(TurretBlueprint tb)
    {
        statsUI.SelectTurret(tb);
    }

}

using UnityEngine;
using UnityEngine.UI;


public class Shop : MonoBehaviour
{
    public StatsUI statsUI;
    public Button[] buttons;

    public TurretBlueprint crossbowTurret;
    public TurretBlueprint cannonTurret;
    //TODO
    public TurretBlueprint laserTurret;
    //public TurretBlueprint antibioticTurret;
    //public TurretBlueprint medicineTurret;

    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.Instance;
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

    void ShowStatsUI(TurretBlueprint tb)
    {
        statsUI.SelectTurret(tb);
    }

}

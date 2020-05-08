using UnityEngine;

public class Shop : MonoBehaviour
{
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
        buildManager.SelectTurretToBuild(crossbowTurret);
    }

    public void SelectCannonTurret()
    {
        buildManager.SelectTurretToBuild(cannonTurret);
    }
    
    public void SelectLaserTurret()
    {
        buildManager.SelectTurretToBuild(laserTurret);
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
}

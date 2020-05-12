using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    public GameObject ui;
    public TMP_Text description;
    public TMP_Text range;
    public TMP_Text damage;
    public TMP_Text fireRate;
    public TMP_Text upgradedRange;
    public TMP_Text upgradedDamage;
    public TMP_Text upgradedFireRate;

    public TurretBlueprint selectedTurret;

    private void Start()
    {
        ui.SetActive(false);
    }

    public void SelectTurret(TurretBlueprint turretBlueprint)
    {
        selectedTurret = turretBlueprint;
        UpdateStats();
    }

    void UpdateStats()
    {
        var turret = selectedTurret.prefab.GetComponent<Turret>() as Turret;
        var upgradedTurret = selectedTurret.upgradedPrefab.GetComponent<Turret>() as Turret;

        description.text = turret.description;
        range.text = turret.range.ToString();
        damage.text = turret.damage.ToString();
        fireRate.text = turret.fireRate.ToString();
        upgradedRange.text = upgradedTurret.range.ToString();
        upgradedDamage.text = upgradedTurret.damage.ToString();
        upgradedFireRate.text = upgradedTurret.fireRate.ToString();

        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

}

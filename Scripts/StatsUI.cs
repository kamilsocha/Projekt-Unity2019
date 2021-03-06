﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays stats of currently selected or hovered turret.
/// </summary>
public class StatsUI : MonoBehaviour
{
    public GameObject ui;
    Image background;
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
        background = GetComponent<Image>();
    }

    public void SelectTurret(TurretBlueprint turretBlueprint)
    {
        selectedTurret = turretBlueprint;
        background.enabled = true;
        UpdateStats();
    }

    void UpdateStats()
    {
        var turret = selectedTurret.prefab.GetComponent<Turret>() as Turret;
        var upgradedTurret = selectedTurret.upgradedPrefab.GetComponent<Turret>() as Turret;

        if (turret is Laser) { Debug.Log("this is a laser"); }

        description.text = turret.description;
        range.text = turret.range.ToString().Replace(",", ".");
        damage.text = turret.damage.ToString().Replace(",", ".");
        fireRate.text = turret.fireRate.ToString().Replace(",", ".");
        upgradedRange.text = upgradedTurret.range.ToString().Replace(",", ".");
        upgradedDamage.text = upgradedTurret.damage.ToString().Replace(",", ".");
        upgradedFireRate.text = upgradedTurret.fireRate.ToString().Replace(",", ".");

        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
        background.enabled = false;
    }

}

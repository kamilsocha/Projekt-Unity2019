using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Controls displayed stats of currently displayed turret on turret panel in menu.
/// </summary>
public class OptionsTurretStats : MonoBehaviour
{
    public TMP_Text description;
    public TMP_Text range;
    public TMP_Text damage;
    public TMP_Text fireRate;
    
    public void SetStats(TurretBlueprint turretBlueprint)
    {
        Turret turret = turretBlueprint.prefab.GetComponent<Turret>();
        Turret upgradedTurret = turretBlueprint.upgradedPrefab.GetComponent<Turret>();
        description.text = turret.description;
        range.text = turret.range.ToString().Replace(",", ".") + " / " + upgradedTurret.range.ToString().Replace(",", ".");
        damage.text = turret.damage.ToString().Replace(",", ".") + " / " + upgradedTurret.damage.ToString().Replace(",", ".");
        fireRate.text = turret.fireRate.ToString().Replace(",", ".") + " / " + upgradedTurret.fireRate.ToString().Replace(",", ".");
    }
}

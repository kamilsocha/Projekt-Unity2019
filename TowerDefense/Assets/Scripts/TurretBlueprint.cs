using UnityEngine;

/// <summary>
/// Contains prefabs of standard and upgraded turret obejct.
/// Provides it's upgrade and sell amount.
/// Allows easier management of shop.
/// </summary>
[System.Serializable]
public class TurretBlueprint
{
    public GameObject prefab;
    public int cost;

    public GameObject upgradedPrefab;
    public int upgradeCost;

    public int GetSellAmount()
    {
        return cost / 2;
    }

    public int GetUpgradedSellAmount()
    {
        return (cost + upgradeCost) / 2;
    }
}

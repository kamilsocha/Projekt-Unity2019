using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{
    public GameObject prefab;
    public int cost;

    public GameObject upgradedPrefab;
    public int upgradeCost;

    public int GetSellAmount()
    {
        //TODO
        return 0;
    }

    public int GetUpgradedSellAmount()
    {
        //TODO
        return 0;
    }
}

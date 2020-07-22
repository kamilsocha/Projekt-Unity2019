using UnityEngine;

/// <summary>
/// Contains all data needed to provide for level.
/// Implemented as scriptable object makes it easier to prepare varius combinations
/// of levels properties.
/// </summary>
[CreateAssetMenu]
public class LevelData : ScriptableObject
{
    public new string name;   
    public Wave[] waves;
    public GameObject bossPrefab;
    public float timeBetweenWaves;
    public int startMoney;
    public int startLives;

    public int scoreToUnlockCannon;
    public int scoreToUnlockLaserBeamer;
    public int scoreToUnlockLaser;
    public int scoreToUnlockAntibiotic;
    public int scoreToUnlockMedicine;
}

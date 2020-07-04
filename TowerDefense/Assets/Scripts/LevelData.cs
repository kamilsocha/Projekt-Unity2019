using UnityEngine;

[CreateAssetMenu]
public class LevelData : ScriptableObject
{
    public new string name;   
    public Wave[] waves;
    public GameObject bossPrefab;
    public float timeBetweenWaves;
    public int startMoney;
    public int startLives;
}

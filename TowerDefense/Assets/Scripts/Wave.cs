using UnityEngine;

[System.Serializable]
public class Wave
{
    public EnemyBlueprint[] enemyWaves;
    public GameObject bossPrefab;
    public int Count { get
        {
            int count = 0;
            foreach(var enemyBlueprint in enemyWaves)
            {
                count += enemyBlueprint.count;
            }
            return count;
        } 
    }
    public float rate;
}

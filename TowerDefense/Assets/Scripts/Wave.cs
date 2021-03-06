﻿/// <summary>
/// Contains waves of enemies spawned during one wave what allows to make more varied
/// levels.
/// </summary>
[System.Serializable]
public class Wave
{
    public EnemyBlueprint[] enemyWaves;
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

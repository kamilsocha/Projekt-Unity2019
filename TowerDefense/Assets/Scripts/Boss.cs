﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Stronger enemy which spawns regular enemies.
/// </summary>
public class Boss : MonoBehaviour
{
    public int livesToReduce = 1;

    public GameObject partToRotate;
    public GameObject enemyPrefabToSpawn;
    public GameObject spawnEffect;
    public GameObject shadowEffectPrefab;
    GameObject shadowEffect;
    public float spawnRange;
    bool front;
    float countdown;
    public float timeBetweenSpawns;
    public int spawns;
    EnemyMovement enemyMovement;
    WaveSpawner waveSpawner;

    private void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        waveSpawner = FindObjectOfType(typeof(WaveSpawner)) as WaveSpawner;
        enemyMovement.LivesToReduce = livesToReduce;
        countdown = timeBetweenSpawns;
        partToRotate = GetComponent<EnemyMovement>().partToRotate;
        front = true;
        if(shadowEffectPrefab != null)
        {
            shadowEffect = Instantiate(shadowEffectPrefab, transform.position, Quaternion.identity);
            shadowEffect.transform.parent = gameObject.transform;
        }
    }

    private void Update()
    {
        if (spawns <= 0) return;

        if(countdown <= 0)
        {
            SpawnChildEnemy();
            waveSpawner.EnemiesAlive++;
            spawns--;
            countdown = timeBetweenSpawns;
            return;
        }
        countdown -= Time.deltaTime;
    }

    /// <summary>
    /// Spawns child enemy and makes it subsribe to OnEndPath event.
    /// </summary>
    void SpawnChildEnemy()
    {
        var pos = GenerateSpawnPosition();
        var spawned = Instantiate(enemyPrefabToSpawn, pos, transform.rotation);
        spawned.GetComponent<EnemyMovement>().SetWaypoint(enemyMovement.GetCurrentWaypoint());

        spawned.GetComponent<EnemyMovement>().OnEndPath += waveSpawner.ReduceEnemies;

        var effect = Instantiate(spawnEffect, pos, transform.rotation);
        Destroy(effect, 5f);
    }

    Vector3 GenerateSpawnPosition()
    {
        Vector3 pos;
        float offset = Random.Range(-spawnRange, spawnRange);

        if(front)
            pos = partToRotate.transform.position + partToRotate.transform.forward * offset;
        else
            pos = partToRotate.transform.position + partToRotate.transform.forward * offset;

        front = !front;
        return pos;
    }

    private void OnDestroy()
    {
        shadowEffect.transform.parent = null;
        Destroy(shadowEffect, 3f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;
    public Wave[] waves;

    public GameManager gameManager;
    public Transform spawnPoint;

    public float timeBeetwenWaves = 5f;
    float countdown = 2f;

    private int waveIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("inside start method");
        EnemiesAlive = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("inside update");
        Debug.Log("inside update, win level");
        if (EnemiesAlive > 0) return;

        if(waveIndex == waves.Length)
        {
            Debug.Log("inside update, win level");
            gameManager.WinLevel();
            this.enabled = false;
        }

        if(countdown <= 0f)
        {
            Debug.Log("inside update, spawn wave");
            StartCoroutine(SpawnWave());
            countdown = timeBeetwenWaves;
            return;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.rounds++;

        Wave wave = waves[waveIndex];
        EnemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            Debug.Log("inside spawn wave, spawn enemy");
            SpawnEnemy(wave.enemyPrefab);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        waveIndex++;
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        Debug.Log("inside spawn enemy, instantiate");
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}

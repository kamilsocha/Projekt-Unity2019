using System.Collections;
using System.Globalization;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public int EnemiesAlive = 0;
    public Wave[] waves;

    public GameManager gameManager;
    PlayerStats playerStats;
    public Transform spawnPoint;
    public Vector3 positionOffset = new Vector3(0, 1.5f, 0);

    public float timeBetweenWaves = 5f;
    float countdown = 2f;
    public TMP_Text waveCountdownText;

    int waveIndex = 0;


    void Start()
    {
        Enemy.OnEnemyDeath += HandleEnemyDeath;
        EnemiesAlive = 0;
        playerStats = GetComponent<PlayerStats>();
    }

    public void StartGame()
    {
        EnemiesAlive = 0;
        waveIndex = 0;
        spawnPoint = GameObject.FindWithTag("SpawnPoint").transform;
        this.enabled = true;
    }

    public void SetData(Wave[] _waves, float _timeBetweenWaves)
    {
        waves = _waves;
        timeBetweenWaves = _timeBetweenWaves;
    }

    void Update()
    {
        if (!GameManager.IsInGame) return;

        if (EnemiesAlive > 0) return;

        if(waveIndex == waves.Length)
        {
            gameManager.WinLevel();
            this.enabled = false;
        }

        if(countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format(CultureInfo.InvariantCulture,"{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        playerStats.Rounds++;
        Wave wave = waves[waveIndex];
        EnemiesAlive = wave.Count + 1;

        Debug.Log($"eniemies alive: {EnemiesAlive}");

        foreach(var enemyWave in wave.enemyWaves)
        {
            for(int i = 0; i < enemyWave.count; i++)
            {
                SpawnEnemy(enemyWave.enemyPrefab);
                yield return new WaitForSeconds(1f / wave.rate);
            }
        }

        while(EnemiesAlive > 1)
        {
            yield return new WaitForSeconds(timeBetweenWaves);
        }

        SpawnEnemy(wave.bossPrefab);

        waveIndex++;
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        var obj = Instantiate(enemyPrefab, spawnPoint.position + positionOffset, spawnPoint.rotation);
        obj.GetComponent<EnemyMovement>().OnEndPath += ReduceEnemies;
    }

    public void ReduceEnemies(int livesToReduce)
    {
        EnemiesAlive--;
        playerStats.ReduceLives(livesToReduce);
    }

    public void HandleEnemyDeath(int money, int score)
    {
        playerStats.RestoreMoney(money);
        playerStats.IncreaseScore(score);
        EnemiesAlive--;
    }
}

using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;
    public Wave[] waves;

    public GameManager gameManager;
    public Transform spawnPoint;
    public Vector3 positionOffset = new Vector3(0, 1.5f, 0);

    public float timeBetweenWaves = 5f;
    float countdown = 2f;
    public Text waveCountdownText;

    private int waveIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        EnemiesAlive = 0;
    }

    public void StartGame()
    {
        EnemiesAlive = 0;
        spawnPoint = GameObject.FindWithTag("SpawnPoint").transform;
        this.enabled = true;
    }

    public void SetData(Wave[] _waves, float _timeBetweenWaves)
    {
        waves = _waves;
        timeBetweenWaves = _timeBetweenWaves;
    }

    // Update is called once per frame
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
        PlayerStats.Rounds++;

        Wave wave = waves[waveIndex];
        EnemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemyPrefab);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        waveIndex++;
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}

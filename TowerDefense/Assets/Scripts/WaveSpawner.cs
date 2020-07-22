using System.Collections;
using System.Globalization;
using UnityEngine;
using TMPro;
using System.Linq;

/// <summary>
/// Spawns waves of enemies, tracks numer of living enemies, handles their deaths.
/// </summary>
public class WaveSpawner : MonoBehaviour
{
    public int EnemiesAlive = 0;
    public Wave[] waves;
    public GameObject bossPrefab;

    public GameManager gameManager;
    PlayerStats playerStats;
    public Transform spawnPoint;
    public Vector3 positionOffset = new Vector3(0, 1.5f, 0);

    public float timeBetweenWaves = 5f;
    float countdown = 2f;
    public TMP_Text waveCountdownText;
    public string[] clockSounds;
    int waveIndex = 0;

    bool wavesSpawned;
    public bool bossSpawned;
    public GameObject waveInfo;
    TMP_Text waveInfoText;
    Animator waveInfoAnimator;
    public string nextWaveText = "next wave";
    public string bossSpawnText = "boss";
    public string clipName = "WaveInfo";
    float lengthOfClip;

    bool isTimerPaused;
    public GameObject timerPaused;

    void Start()
    {
        Enemy.OnEnemyDeath += HandleEnemyDeath;
        EnemiesAlive = 0;
        playerStats = GetComponent<PlayerStats>();

        bossSpawned = false;
        wavesSpawned = false;
        waveInfoText = waveInfo.GetComponentInChildren<TMP_Text>();
        waveInfoAnimator = waveInfo.GetComponent<Animator>();
        lengthOfClip = waveInfoAnimator.runtimeAnimatorController.animationClips.First(x => x.name == clipName).length;

        isTimerPaused = false;
    }

    public void StartGame()
    {
        EnemiesAlive = 0;
        waveIndex = 0;
        bossSpawned = false;
        wavesSpawned = false;
        isTimerPaused = false;
        spawnPoint = GameObject.FindWithTag("SpawnPoint").transform;
        this.enabled = true;
    }

    public void SetData(Wave[] _waves, float _timeBetweenWaves, GameObject _bossPrefab)
    {
        waves = _waves;
        timeBetweenWaves = _timeBetweenWaves;
        bossPrefab = _bossPrefab;
    }

    void Update()
    {
        if (!GameManager.IsInGame) return;

        if (EnemiesAlive > 0) return;

        if(waveIndex == waves.Length)
        {
            if(!bossSpawned)
            {
                waveInfoText.text = bossSpawnText;
                waveInfo.SetActive(true);
                waveInfoAnimator.SetTrigger("Fade");
                StartCoroutine(Wait(lengthOfClip));
                bossSpawned = true;
                StartCoroutine(SpawnBoss());
            }
            else
            {
                gameManager.WinLevel();
                this.enabled = false;
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if(isTimerPaused)
            {
                isTimerPaused = false;
                timerPaused.SetActive(false);
            } else
            {
                isTimerPaused = true;
                timerPaused.SetActive(true);
            }
        }

        if(!wavesSpawned)
        {
            if (isTimerPaused) return;


            if (countdown <= 0f)
            {
                waveInfoText.text = nextWaveText;
                waveInfo.SetActive(true);
                waveInfoAnimator.SetTrigger("Fade");
                StartCoroutine(Wait(lengthOfClip));
                StartCoroutine(SpawnWave());
                countdown = timeBetweenWaves;
                return;
            }

            countdown -= Time.deltaTime;
            countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

            waveCountdownText.text = string.Format(CultureInfo.InvariantCulture, "{0:00.00}", countdown);
        }
        
    }
    /// <summary>
    /// Spawns waves of enemies on regular interval specific for current wave.
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnWave()
    {
        playerStats.Rounds++;
        Wave wave = waves[waveIndex];
        EnemiesAlive = wave.Count;

        if (waveIndex + 1 == waves.Length) wavesSpawned = true;

        foreach (var enemyWave in wave.enemyWaves)
        {
            for(int i = 0; i < enemyWave.count; i++)
            {
                SpawnEnemy(enemyWave.enemyPrefab);
                yield return new WaitForSeconds(1f / wave.rate);
            }
        }

        waveIndex++;
    }
    /// <summary>
    /// Waits for timeBetweenWaves and then spawns the boss enemy.
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnBoss()
    {
        EnemiesAlive++;
        countdown = timeBetweenWaves;

        while (countdown > 0)
        {
            countdown -= Time.deltaTime;
            countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
            waveCountdownText.text = string.Format(CultureInfo.InvariantCulture, "{0:00.00}", countdown);
            yield return 0;
        }

        SpawnEnemy(bossPrefab);
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        waveInfo.SetActive(false);
    }
    /// <summary>
    /// Spawns specified enemyPrefab and subscribes to it's event OnEndPath which occurs
    /// when enemy makes it way to the end of level.
    /// </summary>
    /// <param name="enemyPrefab"></param>
    void SpawnEnemy(GameObject enemyPrefab)
    {
        var obj = Instantiate(enemyPrefab, spawnPoint.position + positionOffset, Quaternion.identity);
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

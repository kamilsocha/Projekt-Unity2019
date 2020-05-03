using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver;
    public static bool IsInGame;

    public GameObject startLevelUI;
    public GameObject gameOverUI;
    public GameObject completeLevelUI;

    public WaveSpawner waveSpawner;
    public PlayerStats playerStats;

    public LevelData[] levels;
    public LevelData currentLevelData;

    [Header("For testing give here name of the level!")]
    public string currentLevel;
    /*[Header("Number of scenes built before out actual levels.", order = 0)]
    [Space(-10, order = 1)]
    [Header("Change if you add new (not level) scene", order = 2)]
    [Space(-10, order = 3)]
    [Header("in build settings.", order = 4)]
    public int offset;*/

    void Start()
    {
        startLevelUI.SetActive(true);

        waveSpawner = GetComponent<WaveSpawner>();
        playerStats = GetComponent<PlayerStats>();

        GameIsOver = false;
        IsInGame = false;
    }

    public void Ready()
    {
        waveSpawner.StartGame();
        IsInGame = true;
    }

    void Update()
    {
        if (GameIsOver) return;

        if(PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        GameIsOver = true;
        completeLevelUI.SetActive(true);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //needs to be commented when testing without level choosing 
        //currentLevel = PlayerPrefs.GetString("CurrentLevel", "Level01");
        if(scene.name == currentLevel)
        {
            currentLevel = PlayerPrefs.GetString("CurrentLevel");//, "Level01");
            Debug.Log("current level = " + currentLevel);
            LevelData ld = Array.Find(levels, level => level.name == currentLevel);
            if (ld == null)
            {
                Debug.LogWarning("Level data not found. You may have not added the level to GameMaster or made a typo :)");
                return;
            }

            waveSpawner.SetData(ld.waves, ld.timeBetweenWaves);
            playerStats.SetData(ld.startMoney, ld.startLives);
            //Ready();
        }
    }

    public void ReadyButton()
    {
        currentLevel = PlayerPrefs.GetString("CurrentLevel");//, "Level01");
        Debug.Log("current level = " + currentLevel);
        LevelData ld = Array.Find(levels, level => level.name == currentLevel);
        if (ld == null)
        {
            Debug.LogWarning("Level data not found. You may have not added the level to GameMaster or made a typo :)");
            return;
        }

        waveSpawner.SetData(ld.waves, ld.timeBetweenWaves);
        playerStats.SetData(ld.startMoney, ld.startLives);

        Ready();
        startLevelUI.SetActive(false);
    }
}

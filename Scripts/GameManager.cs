﻿using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages state of the game. Controls wave spawner, complete/pause level panels.
/// Provides level information for wave spawner, player stats and shop.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static bool GameIsOver;
    public static bool IsInGame;

    public GameObject startLevelUI;
    public GameObject gameOverUI;
    public GameObject completeLevelUI;
    public GameObject gameWonUI;
    public GameObject winLevelEffectPrefab;
    public GameObject winGameEffectPrefab;
    GameObject effectGO;

    public WaveSpawner waveSpawner;
    public PlayerStats playerStats;
    public Shop shop;

    public LevelData[] levels;
    public LevelData currentLevelData;

    [Header("Scene to load after the game is won.")]
    public string gameWonSceneLoad = "MainMenu";
    public delegate void LevelWonEvent();
    public event LevelWonEvent OnLevelWon;

    [Header("For testing give here name of the level!")]
    public string currentLevel;

    public AudioManager audioManager;

    string levelSharedName = "LevelShared";
    string clickAudioName = "ButtonClick";
    public string filePath = "playerStats";

    public delegate void DataSaved();
    public event DataSaved OnDataSaved;

    private void Start()
    {
        waveSpawner = GetComponent<WaveSpawner>();
        playerStats = GetComponent<PlayerStats>();
        shop = FindObjectOfType<Shop>();

        currentLevel = PlayerPrefs.GetString("CurrentLevel", currentLevel);

        completeLevelUI.GetComponent<CompleteLevel>().OnContinue += HandleContinueToNextLevel;
        audioManager = AudioManager.Instance;
    }
    /// <summary>
    /// Prepares data for currently active level.
    /// </summary>
    void OnStart()
    {
        Debug.Log("OnStart called.");
        startLevelUI.SetActive(true);

        GameIsOver = false;
        IsInGame = false;

        Debug.Log("current level = " + currentLevel);
        currentLevelData = Array.Find(levels, level => level.name == currentLevel);
        if (currentLevelData == null)
        {
            Debug.LogWarning("Level data not found. You may have not added the level to GameMaster or made a typo :) From Button");
            return;
        }
        shop.SetData(currentLevelData.scoreToUnlockCannon, currentLevelData.scoreToUnlockLaserBeamer,
            currentLevelData.scoreToUnlockLaser, currentLevelData.scoreToUnlockAntibiotic, currentLevelData.scoreToUnlockMedicine);
        waveSpawner.SetData(currentLevelData.waves, currentLevelData.timeBetweenWaves, currentLevelData.bossPrefab);
        playerStats.SetData(currentLevelData.startMoney, currentLevelData.startLives);
        
    }

    public void Ready()
    {
        waveSpawner.StartGame();
        IsInGame = true;
    }

    void Update()
    {
        if (GameIsOver) return;

        if(playerStats.Lives <= 0)
        {
            Debug.Log($"{playerStats.Lives}");
            EndGame();
        }
    }

    void EndGame()
    {
        Debug.Log("Endgame");
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        OnLevelWon?.Invoke();

        StartCoroutine(TransferMoney());

        GameIsOver = true;
        int levelNumber = Array.IndexOf(levels, currentLevelData);
        if (levelNumber == (levels.Length - 1))
        {
            gameWonUI.SetActive(true);
            effectGO = Instantiate(winGameEffectPrefab, transform.position, Quaternion.identity);
            return;
        }

        effectGO = Instantiate(winLevelEffectPrefab, transform.position, Quaternion.identity);

        levelNumber++;
        currentLevel = levels[levelNumber].name;
        completeLevelUI.GetComponent<CompleteLevel>().SetNextLevel(currentLevel, levelNumber + 1);
        completeLevelUI.SetActive(true);        
    }

    IEnumerator TransferMoney()
    {
        while (playerStats.Money > 0)
        {
            playerStats.ReduceMoney(1);
            playerStats.IncreaseScore(1);
            yield return new WaitForSeconds(0.005f);
        }
        while (playerStats.UpgradesMoney > 0)
        {
            playerStats.UpgradesMoney -= 10;
            playerStats.IncreaseScore(1);
            yield return new WaitForSeconds(0.005f);
        }
        var currentLevelStats = SaveData.Current.GetPlayerLevelData(currentLevelData.name);
        if (playerStats.Score > currentLevelStats.bestScore)
        {
            currentLevelStats.bestScore = playerStats.Score;
        }
        var res = SerializationManager.Save(filePath, SaveData.Current);
        OnDataSaved?.Invoke();
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
        shop = FindObjectOfType<Shop>();
        if (scene.name != levelSharedName)
            OnStart();
    }

    public void ReadyButton()
    {
        Ready();
        SceneFader.Instance.activeScene = currentLevel;
        startLevelUI.SetActive(false);
    }

    public void WinGame()
    {
        SceneFader.Instance.FadeTo(gameWonSceneLoad, LoadType.Menu);
    }
    /// <summary>
    /// Cleans level from turrets and fireworks played after level is won.
    /// </summary>
    void HandleContinueToNextLevel()
    {
        Turret[] turrets = FindObjectsOfType<Turret>();
        foreach (var turret in turrets)
        {
            Destroy(turret.gameObject);
        }
        Destroy(effectGO);
    }

    public void PlaySound(string s)
    {
        if (s == null) s = clickAudioName;
        audioManager.Play(s);
    }
}

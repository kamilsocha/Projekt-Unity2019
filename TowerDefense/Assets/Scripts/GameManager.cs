﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public LevelData[] levels;
    public LevelData currentLevelData;

    [Header("Scene to load after the game is won.")]
    public string gameWonSceneLoad = "MainMenu";

    [Header("For testing give here name of the level!")]
    public string currentLevel;

    public AudioManager audioManager;

    string levelSharedName = "LevelShared";
    string clickAudioName = "ButtonClick";

    private void Start()
    {
        waveSpawner = GetComponent<WaveSpawner>();
        playerStats = GetComponent<PlayerStats>();

        currentLevel = PlayerPrefs.GetString("CurrentLevel", currentLevel);//, "Level01");

        completeLevelUI.GetComponent<CompleteLevel>().OnContinue += HandleContinueToNextLevel;
        audioManager = AudioManager.Instance;
        // delete later - faster loading stuff
        //OnStart();
    }

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
        waveSpawner.SetData(currentLevelData.waves, currentLevelData.timeBetweenWaves);
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
        // TODO
        // calculate overall score

        var currentLevelStats = SaveData.Current.GetPlayerLevelData(currentLevelData.name);
        if(playerStats.Score > currentLevelStats.bestScore)
        {
            currentLevelStats.bestScore = playerStats.Score;
        }

        GameIsOver = true;
        int levelNumber = Array.IndexOf(levels, currentLevelData);
        if (levelNumber == (levels.Length - 1))
        {
            gameWonUI.SetActive(true);
            effectGO = Instantiate(winGameEffectPrefab, transform.position, Quaternion.identity);
            return;
        }

        effectGO = Instantiate(winLevelEffectPrefab, transform.position, Quaternion.identity);

        Debug.Log("Next Level");
        levelNumber++;
        currentLevel = levels[levelNumber].name;
        completeLevelUI.GetComponent<CompleteLevel>().SetNextLevel(currentLevel, levelNumber + 1);
        completeLevelUI.SetActive(true);

        Turret[] turrets = FindObjectsOfType<Turret>();
        foreach (var turret in turrets)
        {
            Destroy(turret.gameObject);
        }
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
        if(scene.name != levelSharedName)
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

    void HandleContinueToNextLevel()
    {
        Destroy(effectGO);
    }

    public void PlaySound(string s)
    {
        if (s == null) s = clickAudioName;
        audioManager.Play(s);
    }
}

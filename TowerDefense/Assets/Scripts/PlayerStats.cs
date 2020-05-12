﻿using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int startMoney = 400;
    public static int Money { get; set; }

    public int startLives = 20;
    public static int Lives { get; set; }

    public static int Rounds { get; set; }
    
    // TODO
    // Points system
    public static float points { get; set; }

    public delegate void PlayerMoneyChangedEvent();
    public event PlayerMoneyChangedEvent OnPlayerMoneyChanged;

    void Start()
    {
        Money = startMoney;
        Lives = startLives;

        Rounds = 0;
        points = 0;
    }

    public void SetData(int _startMoney, int _startLives)
    {
        startMoney = _startMoney;
        startLives = _startLives;
    }

    public void ReduceMoney(int moneyToReduce)
    {
        Money -= moneyToReduce;
        OnPlayerMoneyChanged?.Invoke();
    }

    public void RestoreMoney(int moneyToRestore)
    {
        Money += moneyToRestore;
        OnPlayerMoneyChanged?.Invoke();
    }

    //TODO?
    //public void ReduceLive() { }
}

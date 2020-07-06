using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int startMoney = 400;
    public int Money { get; set; }

    public int startLives = 20;
    public int Lives;// { get; set; }

    public int Rounds { get; set; }
    
    // TODO
    // Score system
    public int Score { get; set; }

    public int UpgradesMoney;// { get; set; }

    public delegate void PlayerMoneyChangedEvent(int money);
    public event PlayerMoneyChangedEvent OnPlayerMoneyChanged;
    public delegate void PlayerLivesChangedEvent(int lives);
    public event PlayerLivesChangedEvent OnPlayerLivesChanged;
    public delegate void PlayerScoreChangedEvent(int lives);
    public event PlayerScoreChangedEvent OnPlayerScoreChanged;

    void Start()
    {
        Money = startMoney;
        Lives = startLives;

        Rounds = 0;
        Score = 0;
    }

    public void SetData(int _startMoney, int _startLives)
    {
        startMoney = _startMoney;
        startLives = _startLives;
        Money = startMoney;
        Lives = startLives;
        Rounds = 0;
        Score = 0;

        OnPlayerLivesChanged?.Invoke(Lives);
        OnPlayerMoneyChanged?.Invoke(Money);
        OnPlayerScoreChanged?.Invoke(Score);
    }

    public void ReduceMoney(int moneyToReduce)
    {
        Money -= moneyToReduce;
        OnPlayerMoneyChanged?.Invoke(Money);
    }

    public void RestoreMoney(int moneyToRestore)
    {
        Money += moneyToRestore;
        OnPlayerMoneyChanged?.Invoke(Money);
    }

    public void ReduceLives(int livesToReduce) 
    {
        Lives -= livesToReduce;
        OnPlayerLivesChanged?.Invoke(Lives);
    }

    public void IncreaseScore(int scoreToAdd)
    {
        Score += scoreToAdd;
        OnPlayerScoreChanged?.Invoke(Score);
    }

    public void UpgradeTurret(int money)
    {
        UpgradesMoney += money;
        RestoreMoney(money);
    }
}

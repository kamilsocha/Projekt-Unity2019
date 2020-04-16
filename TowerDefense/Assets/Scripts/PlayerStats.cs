using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int startMoney = 400;
    public static int Money { get; set; }

    public int startLives = 20;
    public static int lives { get; set; }

    public static int rounds { get; set; }
    
    // TODO
    // Points system
    public static float points { get; set; }

    void Start()
    {
        Money = startMoney;
        lives = startLives;

        rounds = 0;
        points = 0;
    }
}

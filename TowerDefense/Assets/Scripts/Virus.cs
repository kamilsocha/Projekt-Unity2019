using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : Enemy
{
    bool canRevive;
    int revives;
    [Header("Maximum number of times the virus can revive.")]
    [Range(0, 5)]
    public int maxRevives = 0;
    [Header("Probability to get bonus health points.")]
    [Range(0, 100)]
    public int chanceToRevive;
    [Header("Health to receive if virus survives.")]
    public int reviveHealth;

    protected override void Start()
    {
        base.Start();
        canRevive = false;
        revives = maxRevives;
    }

    protected override void Die()
    {
        float revive = Random.Range(0, 100);
        if(revive <= chanceToRevive && canRevive)
        {
            health = reviveHealth;
            healthBar.fillAmount = reviveHealth / (float)startHealth;
            revives--;
            if(revives <= 0)
            {
                canRevive = false;
            } 
        }
        else
        {
            base.Die();
        }
    }
}

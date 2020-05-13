using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : Enemy
{
    bool hasRevived;
    [Header("Probability to get bonus health points")]
    [Range(0f, 100f)]
    public int chanceToRevive;
    [Header("Health to receive if virus survives.")]
    public int reviveHealth;

    protected override void Start()
    {
        base.Start();
        hasRevived = false;
    }

    protected override void Die()
    {
        float revive = Random.Range(0, 100);
        if(revive <= chanceToRevive && !hasRevived)
        {
            health = reviveHealth;
            hasRevived = true;
            healthBar.fillAmount = reviveHealth / startHealth;
        }
        else
        {
            base.Die();
        }
    }
}

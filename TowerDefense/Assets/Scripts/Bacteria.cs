using UnityEngine;

public class Bacteria : Enemy
{
    bool isShieldActive;    // if shield isn't destroyed, false = is destroyed
    bool antibioticEffect;  // helps enabling effect of antibiotic turret
    [Header("Total amount of damage the shield can take before it disappears.")]
    public int startShieldDurability;   // start value of shield durability
    float shieldDurability;             // actual value of shield durability
    [Header("Value of damage the shield absorbs.")]
    public int shieldStart;             // start value of shield strength
    float shield;                       // actual value of shield strength

    protected override void Start()
    {
        base.Start();
        shield = shieldStart;
        shieldDurability = startShieldDurability;
        isShieldActive = true;
        InvokeRepeating("ShieldRepair", 0f, 3f);    // every 3 sec -> repairing shield
    }

    void ShieldRepair()
    {
        shieldDurability += (float)(startShieldDurability * 0.1);   // repairing 10% of shield
        if(shieldDurability > startShieldDurability)                // if over 100% -> stop on max
        {
            shieldDurability = startShieldDurability;
        }
    }
    public void EnableAntibioticEffect()
    {
        antibioticEffect = true;    // enabling effect of antibiotic turret
    }
    public void DisableAntibioticEffect()
    {
        antibioticEffect = false;   // disabling effect of antibiotic turret
    }

    public override void TakeDamage(float amount)
    {
        if ((isShieldActive) && (antibioticEffect == false))    // if bacteria has shield and there is no antibiotic turret effect
        {
            float amountToReduce = amount - shield;
            if (amountToReduce > 0)
            {
                health -= amountToReduce;
                shieldDurability -= amountToReduce;
                healthBar.fillAmount = health / (float)startHealth;
            }
            else 
            {
                shieldDurability -= amount;
            }
        }
        else    // normal taking damage
        {
            health -= amount;
            healthBar.fillAmount = health / (float)startHealth;
        }
        if(shieldDurability <= 0)   // if durability below 0 -> disable shield
        {
            DisableShield();    
        }
        if(health < 0 && isAlive)
        {
            Die();
        }
    }

    public void DisableShield()
    {
        CancelInvoke("ShieldRepair");   // stop invoke every 3 sec of repairing shield
        isShieldActive = false;         // for future TakeDamage()
    }

}

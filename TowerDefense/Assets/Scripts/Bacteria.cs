using UnityEngine;

public class Bacteria : Enemy
{
    bool isShieldActive;
    bool antibioticEffect;
    [Header("Total amount of damage the shield can take before it disappears.")]
    public int startShieldDurability;
    float shieldDurability;
    [Header("Value of damage the shield absorbs.")]
    public int shieldStart;
    float shield;

    protected override void Start()
    {
        base.Start();
        shield = shieldStart;
        shieldDurability = startShieldDurability;
        isShieldActive = true;
        InvokeRepeating("ShieldRepair", 0f, 3f);
    }

    void ShieldRepair()
    {
        shieldDurability += (float)(startShieldDurability * 0.1); 
        if(shieldDurability > startShieldDurability)
        {
            shieldDurability = startShieldDurability;
        }
    }
    public void EnableAntibioticEffect()
    {
        antibioticEffect = true;
    }
    public void DisableAntibioticEffect()
    {
        antibioticEffect = false;
    }

    public override void TakeDamage(float amount)
    {
        if ((isShieldActive) && (antibioticEffect == false))
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
        else
        {
            health -= amount;
            healthBar.fillAmount = health / (float)startHealth;
        }
        if(shieldDurability <= 0)
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
        isShieldActive = false;
        CancelInvoke("ShieldRepair");
    }

}

using UnityEngine;

public class Bacteria : Enemy
{
    bool isShieldActive;
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
    }

    public override void TakeDamage(float amount)
    {
        if (isShieldActive)
        {
            Debug.Log(amount);
            
            float amountToReduce = amount - shield;
            Debug.Log(amountToReduce);
            if (amountToReduce > 0)
            {
                health -= amountToReduce;
                shieldDurability -= amountToReduce;
            }
            else 
            {
                shieldDurability -= amount;
            }
        }
        else
        {
            health -= amount;
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
    }

}

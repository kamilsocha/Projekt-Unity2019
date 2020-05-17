using UnityEngine;

public class Bacteria : Enemy
{
    bool isShieldActive;
    [Header("Total amount of damage the shield can take before it disappears.")]
    public int shieldDurability;
    [Header("Value of damage the shield absorbs.")]
    public int shield;

    protected override void Start()
    {
        base.Start();
        isShieldActive = true;
    }

    public override void TakeDamage(int amount)
    {
        if (isShieldActive)
        {
            Debug.Log(amount);
            
            int amountToReduce = amount - shield;
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

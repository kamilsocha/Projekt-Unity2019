

public class Bacteria : Enemy
{
    bool isShieldActive = true;
    public int shieldDurability;
    public int shield;

    public override void TakeDamage(int amount)
    {
        if(isShieldActive)
        {
            int amountToReduce = amount - shield;
            if(amountToReduce > 0)
            {
                health -= amountToReduce;
                shieldDurability -= amountToReduce;
            }
            else 
            {
                shieldDurability -= amount / 2;
            }
        }
        else
        {
            health -= amount;
        }
        if(shieldDurability >= 0)
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

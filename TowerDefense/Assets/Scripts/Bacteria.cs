

public class Bacteria : Enemy
{
    bool isShieldActive = true;
    public int shield;

    public override void TakeDamage(int amount)
    {
        if(isShieldActive)
        {
            amount -= shield;
            if(amount > 0)
            {
                health -= amount;
            }
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

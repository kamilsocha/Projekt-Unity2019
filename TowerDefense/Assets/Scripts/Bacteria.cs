

public class Bacteria : Enemy
{
    bool isShieldActive = true;
    public float shield;

    public override void TakeDamage(float amount)
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

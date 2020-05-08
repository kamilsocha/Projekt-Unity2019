using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 5f;
    [HideInInspector]
    public float speed;
    bool isAlive = true;
    float startHealth = 100;
    public float health;
    public int worth = 50;
    public GameObject deathEffect;

    [Header("Unity Stuff")]
    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    public void TakeDamage(float amount)
    {
        Debug.Log(gameObject.name.ToString() + " Taking damage");

        health -= amount;
        healthBar.fillAmount = health / startHealth;
                
        if(health <= 0 && isAlive)
        {
            Die();
        }
    }

    public void Slow(float percentage)
    {
        speed = startSpeed * (1f - percentage);
    }

    void Die()
    {
        isAlive = false;
        PlayerStats.Money += worth;
        WaveSpawner.EnemiesAlive--;

        GameObject deathEffectGO = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(deathEffectGO, 5f);

        Destroy(gameObject);
    }
}

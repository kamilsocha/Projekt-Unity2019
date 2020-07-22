using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

/// <summary>
/// Basic class describing enemy, contains properties common for bacteria and virus types of enemies.
/// Provides common methods.
/// </summary>
public abstract class Enemy : MonoBehaviour
{
    public float startSpeed = 5f;
    [HideInInspector]
    public float speed;
    protected bool isAlive = true;
    public int startHealth = 100;
    protected float health;
    public int moneyWorth = 50;
    public int scoreWorth = 10;
    public GameObject deathEffect;
    public GameObject bloodPrefab;

    [Header("Unity Stuff")]
    public Image healthBar;

    public delegate void EnemyDeathEvent(int moneyWorth, int scoreWorth);
    public static event EnemyDeathEvent OnEnemyDeath;

    protected virtual void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }
    /// <summary>
    /// Takes damage and displays left amount of live on health bar.
    /// </summary>
    /// <param name="amount">value of damage to take</param>
    public virtual void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / (float) startHealth;
                
        if(health <= 0 && isAlive)
        {
            Die();
        }
    }

    public void Slow(float percentage)
    {
        percentage /= 100f;
        speed = startSpeed * (1f - percentage);
        if(speed < (float)(startSpeed * 0.3))
        {
            speed = (float)(startSpeed * 0.3);
        }
    }

    public void EndSlow()
    {
        speed = startSpeed;
    }

    protected virtual void Die()
    { 
        isAlive = false;

        OnEnemyDeath?.Invoke(moneyWorth, scoreWorth);

        GameObject bloodEffect = Instantiate(bloodPrefab, transform.position, Quaternion.identity);
        Destroy(bloodEffect, 3f);
        GameObject deathEffectGO = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(deathEffectGO, 5f);

        Destroy(gameObject);
    }
}

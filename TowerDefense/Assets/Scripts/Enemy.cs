﻿using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    public float startSpeed = 5f;
    [HideInInspector]
    public float speed;
    protected bool isAlive = true;
    public int startHealth = 100;
    //[HideInInspector]
    public float health;
    public int worth = 50;
    public GameObject deathEffect;

    [Header("Unity Stuff")]
    public Image healthBar;

    protected virtual void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

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
    }

    protected virtual void Die()
    {
        isAlive = false;
        FindObjectOfType<PlayerStats>().RestoreMoney(worth);
        WaveSpawner.EnemiesAlive--;

        GameObject deathEffectGO = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(deathEffectGO, 5f);

        Destroy(gameObject);
    }
}

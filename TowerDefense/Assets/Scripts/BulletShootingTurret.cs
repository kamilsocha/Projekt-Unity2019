﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShootingTurret : ShootingTurret
{
    Animator animator;
    public GameObject bulletPrefab;
    float fireCountdown = 0f;

    protected override void Start()
    {
        base.Start();
        animator = GetComponentInChildren<Animator>();
        bulletPrefab.GetComponent<Bullet>().damage = damage;
    }

    protected override void Update()
    {
        base.Update();
        if (target == null)
        {
            return;
        }

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        animator.SetTrigger("Shoot");
        audioManager.Play(audioEffectName);

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }
}
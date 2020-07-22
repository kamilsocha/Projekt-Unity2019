using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Type of turret which shoots bullets on some regular basis.
/// </summary>
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

        if (bullet != null)
        {
            bullet.Seek(target);
        }

        animator.SetTrigger("Shoot");
        audioManager.Play(audioEffectName);

        
    }
}

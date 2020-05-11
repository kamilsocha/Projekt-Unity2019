using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetSeeker))]
public class ShootingTurret : Turret
{
    public enum TurretType { ShootingBullets, Laser };
    public TurretType type;

    public Animator animator;
    //[Header("Use Bullets(default)")]
    public GameObject bulletPrefab;
    float fireCountdown = 0f;

    //[Header("Use Laser")]
    //public bool useLaser = false;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;
    //public int laserDamage = 20;
    public float laserSpeed = 20f;

    //[Header("Unity Setup Stuff")]
    public string enemyTag = "Enemy";
    public Transform firePoint;

    Transform target;
    TargetSeeker targetSeeker;
    
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        targetSeeker = GetComponent<TargetSeeker>();
        if(bulletPrefab != null) 
            bulletPrefab.GetComponent<Bullet>().damage = damage;
    }
    
    void Update()
    {
        target = targetSeeker.Target;
        if(target == null)
        {
            return;
        }

        if (fireCountdown <= 0f)
        {
            //if(useLaser)
            if(type == TurretType.Laser)
            {
                Laser();
            }
            else
            {
                Shoot();
                //fireCountdown = 1f / fireRate;
            }
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGO =  Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        animator.SetTrigger("Shoot");

        if(bullet != null)
        {
            bullet.Seek(target);
        }
    }

    void Laser()
    {
        RaycastHit[] hits;
        Vector3 dir = firePoint.position - target.position;

        hits = Physics.RaycastAll(firePoint.position, firePoint.forward, range);
        Debug.Log("hits: " + hits.Length);
        if (hits.Length > 0)
        {
            RaycastHit farestTarget = hits[0];

            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                Enemy hitEnemy = hit.transform.GetComponent<Enemy>();
                if (hitEnemy)
                {
                    hitEnemy.TakeDamage(damage);
                    if (hit.distance > farestTarget.distance)
                        farestTarget = hit;
                }
            }
            if(farestTarget.transform.tag == "Enemy")
                StartCoroutine(DrawLaser(farestTarget.transform.position));
        }
    }

    IEnumerator DrawLaser(Vector3 finalPosition)
    {

        Vector3 pos = firePoint.position;

        lineRenderer.SetPosition(0, pos);
        lineRenderer.SetPosition(1, finalPosition);
        lineRenderer.enabled = true;

        Vector3 dir = finalPosition - firePoint.position;
        

        while (true)
        {
            lineRenderer.SetPosition(0, pos);  
            float distanceThisFrame = laserSpeed * Time.deltaTime;
            if ((finalPosition - pos).magnitude <= distanceThisFrame) break;
            pos += dir.normalized * distanceThisFrame;

            yield return 0;
        }
        lineRenderer.enabled = false;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}

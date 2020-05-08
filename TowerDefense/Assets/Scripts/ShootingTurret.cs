using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTurret : MonoBehaviour
{
    Transform target;

    Animator animator;

    [Header("General")]
    public float range = 15f;
    public float fireRate = 1f;

    [Header("Use Bullets(default)")]
    float fireCountdown = 0f;
    public GameObject bulletPrefab;

    [Header("Use Laser")]
    public bool useLaser = false;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;
    public float damage = 20f;
    public float laserSpeed = 20f;

    [Header("Unity Setup Stuff")]
    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public Transform firePoint;
    public float turnSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        animator = partToRotate.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            return;
        }
        LockOnTarget();



        if (fireCountdown <= 0f)
        {
            if(useLaser)
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

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach  (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
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

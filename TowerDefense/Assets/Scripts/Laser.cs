using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : ShootingTurret
{
    public GameObject laserPrefab;
    LineRenderer lineRenderer;
    float fireCountdown = 0f;

    protected override void Start()
    {
        base.Start();
        laserPrefab.SetActive(false);
        lineRenderer = laserPrefab.GetComponentInChildren<LineRenderer>();
    }

    protected override void Update()
    {
        base.Update();
        if(target == null)
        {
            return;
        }

        if (fireCountdown <= 0f)
        {
            FireLaser();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    void FireLaser()
    {
        RaycastHit[] hits;

        hits = Physics.RaycastAll(firePoint.position, firePoint.forward, range);
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
            if (farestTarget.transform.tag == "Enemy")
            {
                StartCoroutine(DrawLaser(farestTarget.transform.gameObject));
                audioManager.Play(audioEffectName);
            }
                
        }
    }

    IEnumerator DrawLaser(GameObject enemyGO)
    {
        laserPrefab.SetActive(true);
        float normalizedTime = 0;

        while (normalizedTime <= 1f)
        {
            normalizedTime += Time.deltaTime;
            laserPrefab.transform.position = firePoint.position;
            laserPrefab.transform.rotation = firePoint.rotation;
            if (enemyGO != null)
                lineRenderer.SetPosition(1, laserPrefab.transform.InverseTransformPoint(enemyGO.transform.position));
            else
                break;
            yield return 0;
        }

        laserPrefab.SetActive(false);
    }
}

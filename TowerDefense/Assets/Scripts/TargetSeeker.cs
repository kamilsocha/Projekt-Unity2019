using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Responsible for tracking nearest target if it stays in turrets range.
/// </summary>
[RequireComponent(typeof(ShootingTurret))]
public class TargetSeeker : MonoBehaviour
{
    public Transform Target { get; private set; }
    public Enemy TargetEnemy { get; private set; }
    public Transform partToRotate;
    public float turnSpeed = 10f;

    private ShootingTurret turret;

    void Start()
    {
        turret = GetComponent<ShootingTurret>();
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    private void Update()
    {
        if (Target == null) return;
        LockOnTarget();
    }
    /// <summary>
    /// Regular updating of closest enemy.
    /// </summary>
    void UpdateTarget()
    {
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (string tag in turret.enemyTags)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }
        }
        if (nearestEnemy != null && shortestDistance <= turret.range)
        {
            Target = nearestEnemy.transform;
            TargetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            Target = null;
        }
    }
    /// <summary>
    /// Smoothly rotates in direction of current target.
    /// </summary>
    void LockOnTarget()
    {
        Vector3 dir = Target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles; // smooth move to target direction
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

}

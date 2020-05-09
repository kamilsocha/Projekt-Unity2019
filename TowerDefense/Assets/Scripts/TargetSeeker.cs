using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShootingTurret))]
public class TargetSeeker : MonoBehaviour
{
    public Transform Target { get; private set; }
    public Transform partToRotate;
    public float turnSpeed = 10f;

    private ShootingTurret turret;

    // Start is called before the first frame update
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

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(turret.enemyTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            //var bacteria = enemy.GetComponent<Enemy>() as Bacteria;
            //if (bacteria != null && !bacteria.canHurt) continue;
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= turret.range)
        {
            Target = nearestEnemy.transform;
        }
        else
        {
            Target = null;
        }
    }

    void LockOnTarget()
    {
        Vector3 dir = Target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles; // smooth move to target direction
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

}

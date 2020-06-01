using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetSeeker))]
public class ShootingTurret : Turret
{
    public string enemyTag = "Enemy";
    public Transform firePoint;

    protected Transform target;
    protected Enemy targetEnemy;
    TargetSeeker targetSeeker;
    
    protected virtual void Start()
    {
        targetSeeker = GetComponent<TargetSeeker>();
    }
    
    protected virtual void Update()
    {
        target = targetSeeker.Target;
        targetEnemy = targetSeeker.TargetEnemy;
    }

}

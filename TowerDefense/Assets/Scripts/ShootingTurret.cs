using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetSeeker))]
public class ShootingTurret : Turret
{
    public string[] enemyTags = { "Enemy", "Bacteria" };
    public string audioEffectName = "Shot";
    public Transform firePoint;

    protected Transform target;
    protected Enemy targetEnemy;
    TargetSeeker targetSeeker;
    protected AudioManager audioManager;
    
    protected virtual void Start()
    {
        targetSeeker = GetComponent<TargetSeeker>();
        audioManager = AudioManager.Instance;
    }
    
    protected virtual void Update()
    {
        target = targetSeeker.Target;
        targetEnemy = targetSeeker.TargetEnemy;
    }

}

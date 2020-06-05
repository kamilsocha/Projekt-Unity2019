using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeamer : ShootingTurret
{
    [Range(0, 100)]
    public int slowAmount;

    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    protected override void Update()
    {
        base.Update();
        if(target == null)
        {
            if(lineRenderer.enabled)
            {
                lineRenderer.enabled = false;
                impactEffect.Stop();
                impactLight.enabled = false;
                audioManager.Stop(audioEffectName);
            }
            return;
        }

        LaserBeam();   
    }

    void LaserBeam()
    {
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
            audioManager.Play(audioEffectName);
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;
        impactEffect.transform.position = target.position + dir.normalized;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);

        targetEnemy.TakeDamage(damage * Time.deltaTime);
        targetEnemy.Slow(slowAmount);
    }
}

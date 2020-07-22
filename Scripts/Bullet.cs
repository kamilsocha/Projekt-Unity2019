using UnityEngine;

/// <summary>
/// Controls bullet behaviour. 
/// Chases enemy and hurts it or destroys itself if target object already doesn't exist.
/// Can hurt one enemy or explode hurting all enemies within explosion radius.
/// </summary>
public class Bullet : MonoBehaviour
{
    Transform target;
    public float speed = 50f;
    public float damage = 50f;
    public float explosionRadius = 0f;
    public GameObject impactEffect;

    /// <summary>
    /// Bullet follows target as long as it exists or bullet reaches target.
    /// Magnitude vector describing bullets distance to enemy allows to determine if bullet
    /// will hit enemy in current frame, if it does the target is hit.
    /// This condition is checked so that the bullet doesn't go too far.
    /// </summary>
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }
    /// <summary>
    /// When target is hit impact effect is instantiated and according to bullets range
    /// right action is taken - explosion or damaging.
    /// </summary>
    void HitTarget()
    {
        GameObject effectInstance = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 2f);
        
        if(explosionRadius > 0f)
        {
            Explode();
        } 
        else
        {
            Damage(target);
        }
        
        Destroy(gameObject);
    }

    void Damage(Transform enemy)
    {
        Enemy _enemy = enemy.GetComponent<Enemy>();
        if (_enemy != null)
        {
            _enemy.TakeDamage(damage);
        }
    }
    /// <summary>
    /// Checks for all colliders with enemy tag within range and hurts them.
    /// </summary>
    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if(collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}

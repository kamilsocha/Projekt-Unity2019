using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform target;
    public float speed = 50f;
    public float damage = 50f;
    public float explosionRadius = 0f;
    public GameObject impactEffect;

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

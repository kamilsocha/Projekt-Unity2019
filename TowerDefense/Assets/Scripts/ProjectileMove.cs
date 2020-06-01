using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public float speed;
    public GameObject impactPrefab;
    public List<GameObject> trails;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(speed != 0 && rb != null)
        {
            rb.position += transform.forward * speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        speed = 0;

        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;

        if(impactPrefab != null)
        {
            var impactGO = Instantiate(impactPrefab, pos, rot);

            Destroy(impactGO, 300);
        }

        if(trails.Count > 0)
        {
            for(int i = 0; i < trails.Count; i++)
            {
                trails[i].transform.parent = null;
                var particleSystem = trails[i].GetComponent<ParticleSystem>();
                if(particleSystem != null)
                {
                    particleSystem.Stop();
                    Destroy(particleSystem.gameObject, particleSystem.main.duration + particleSystem.main.startLifetime.constantMax);
                }
            }
        }

        Destroy(gameObject);
    }
}

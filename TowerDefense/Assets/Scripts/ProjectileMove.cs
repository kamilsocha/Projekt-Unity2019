using System;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour, IPooledObject
{
    public float speedBoundLow = 30;
    public float speedBoundHigh = 70;
    public float speed;
    public GameObject impactPrefab;
    public List<GameObject> trails;

    private Rigidbody rb;

    public void OnObjectSpawn()
    {
        speed = UnityEngine.Random.Range(speedBoundLow, speedBoundHigh);
        if (trails.Count > 0)
        {
            for (int i = 0; i < trails.Count; i++)
            {
                trails[i].transform.parent = gameObject.transform;
                trails[i].transform.position = transform.position;
            }
        }
    }

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

            Destroy(impactGO, 150);
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
                }
            }
        }
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        gameObject.SetActive(false);
    }

}

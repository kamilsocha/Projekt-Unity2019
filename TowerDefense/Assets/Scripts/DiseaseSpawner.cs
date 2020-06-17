using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiseaseSpawner : MonoBehaviour
{
    public ObjectPooler objectPooler;
    public string poolName = "Diseases";

    public float timeBetweenSpawns = 10f;
    public float sideForce = 10f;
    public float upForce = 10f;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
        InvokeRepeating("SpawnDisease", timeBetweenSpawns, timeBetweenSpawns);
    }

    void SpawnDisease()
    {
        float xForce = Random.Range(-sideForce, sideForce);
        float yForce = Random.Range(upForce / 2f, upForce);
        float zForce = Random.Range(-sideForce, sideForce);

        Vector3 force = new Vector3(xForce, yForce, zForce);

        var diseaseObj = objectPooler.SpawnFromPool(poolName, transform.position, transform.rotation);
        diseaseObj.GetComponent<Rigidbody>().AddForce(force);
        StartCoroutine(Deactivate(diseaseObj, timeBetweenSpawns));
    }

    IEnumerator Deactivate(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
    }

}

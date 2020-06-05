﻿using System.Collections;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    //public GameObject meteorPrefab;
    public Transform spawnPosition;
    Transform endPosition;
    ObjectPooler objectPooler;

    bool doSpawn;
    int spawns = 0;
    int maxNumberOfSpawns = 10;
    public float timeBetweenSpawns = 60;
    public float spawnDelay = 10;
    float countdown;

    float min_x, max_x;
    float min_z, max_z;

    void Start()
    {
        endPosition = transform;       

        min_x = transform.position.x - GetComponent<Collider>().bounds.size.x / 2;
        max_x = transform.position.x + GetComponent<Collider>().bounds.size.x / 2;
        min_z = transform.position.z - GetComponent<Collider>().bounds.size.z / 2;
        max_z = transform.position.z + GetComponent<Collider>().bounds.size.z / 2;
    }

    private void Update()
    {
        StartCoroutine(DelaySpawning());

        if (!doSpawn)
        {
            return;
        }

        if(countdown <= 0)
        {
            SpawnMeteor();
            countdown = timeBetweenSpawns;
            return;
        }

        countdown -= Time.deltaTime;
    }

    Vector3 GenerateSpawnPosition()
    {
        var startX = Random.Range(min_x, max_x);
        var startZ = Random.Range(min_z, max_z);
        return new Vector3(startX, spawnPosition.position.y, startZ);
    }

    Vector3 GenerateEndPosition()
    {
        var endX = Random.Range(min_x, max_x);
        var endZ = Random.Range(min_z, max_z);
        return new Vector3(endX, endPosition.position.y, endZ);
    }

    void RotateTo(GameObject obj, Vector3 destination)
    {
        var direction = destination - obj.transform.position;
        var rotation = Quaternion.LookRotation(direction);
        obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1);
    }

    void SpawnMeteor()
    {
        var startPos = GenerateSpawnPosition();
        var endPos = GenerateEndPosition();
        //GameObject meteor = Instantiate(meteorPrefab, startPos, Quaternion.identity);
        //GameObject meteor = ObjectPooler.Instance.SpawnFromPool("M", startPos, Quaternion.identity);
        GameObject meteor = objectPooler.SpawnFromPool("Meteors", startPos, Quaternion.identity);
        RotateTo(meteor, endPos);
        spawns++;
        if (spawns >= maxNumberOfSpawns) 
            doSpawn = false;

    }

    IEnumerator DelaySpawning()
    {
        yield return new WaitForSeconds(spawnDelay);
        objectPooler = ObjectPooler.Instance;
        doSpawn = true;
    }

}
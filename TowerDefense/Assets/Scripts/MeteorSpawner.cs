using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public GameObject meteorPrefab;
    public Transform spawnPosition;
    public Transform endPosition;

    bool doSpawn;
    int spawns = 0;
    int maxNumberOfSpawns = 10;
    public float timeBetweenSpawns;
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

        doSpawn = true;
    }

    private void Update()
    {
        if (!doSpawn)
        {
            //destroy?
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

    void RotateTo(GameObject go, Vector3 destination)
    {
        var direction = destination - go.transform.position;
        var rotation = Quaternion.LookRotation(direction);
        go.transform.localRotation = Quaternion.Lerp(go.transform.rotation, rotation, 1);
    }

    void SpawnMeteor()
    {
        var startPos = GenerateSpawnPosition();
        var endPos = GenerateEndPosition();
        GameObject meteorGO = Instantiate(meteorPrefab, startPos, Quaternion.identity);
        RotateTo(meteorGO, endPos);
        spawns++;
        if (spawns >= maxNumberOfSpawns) 
            doSpawn = false;
    }

}

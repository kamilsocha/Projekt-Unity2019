using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Simulates random wandering of red blood cells in pool using navmeshagent.
/// </summary>
public class RandomWandering : MonoBehaviour
{
    NavMeshAgent nav;
    public float xRange;
    public float zRange;
    public float speed = 0.3f;
    public int timeToFindDestination;
    public Transform targetObject;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        nav.speed = speed;
        InvokeRepeating("SetDestination", 0, timeToFindDestination);
    }

    void SetDestination()
    {
        Vector3 target = FindTarget();
        nav.SetDestination(target);
    }

    Vector3 FindTarget()
    {
        float x = Random.Range(-xRange, xRange);
        float z = Random.Range(-zRange, zRange);
        targetObject.localPosition = new Vector3(x, targetObject.localPosition.y, z);
        return targetObject.position;
    }
}

using System;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    public GameObject partToRotate;
    public string enemyEndPathSound = "EnemyEndPath";

    Transform target;
    int waypointIndex = 0;
    float radius = 0.4f;

    Enemy enemy;
    public int LivesToReduce { get; set; }

    public delegate void OnEndPathAction(int livesToReduce);
    public event OnEndPathAction OnEndPath;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        target = Waypoints.points[0];
        LivesToReduce = 1;
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);
        RotateToTarget();

        if (Vector3.Distance(transform.position, target.position) <= radius)
        {
            GetNextWaypoint();
        }
        enemy.speed = enemy.startSpeed;
    }

    void GetNextWaypoint()
    {
        if(waypointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }
        waypointIndex++;
        target = Waypoints.points[waypointIndex];
    }

    void RotateToTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.transform.rotation, lookRotation, Time.deltaTime * 20).eulerAngles;
        partToRotate.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void EndPath()
    {
        OnEndPath?.Invoke(LivesToReduce);
        AudioManager.Instance.Play(enemyEndPathSound);
        Destroy(gameObject);
    }

    public int GetCurrentWaypoint()
    {
        return waypointIndex;
    }

    public void SetWaypoint(int index)
    {
        waypointIndex = index - 1;
        GetNextWaypoint();
    }

}

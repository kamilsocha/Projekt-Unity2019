using System;
using UnityEngine;

/// <summary>
/// Manages enemies movement - follows waypoints.
/// </summary>
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
    /// <summary>
    /// Takes another target to get to. If there are no more target it ends path.
    /// </summary>
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
    /// <summary>
    /// Rotates towards the next waypoint.
    /// Part to rotate is the actual enemy body which needs to be rotated.
    /// Health bar stays rotated the way it should.
    /// </summary>
    void RotateToTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.transform.rotation, lookRotation, Time.deltaTime * 20).eulerAngles;
        partToRotate.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    /// <summary>
    /// If enemy reaches the end of path player's lives need to be reduced
    /// and enemy can be destroyed.
    /// </summary>
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
    /// <summary>
    /// Sets new waypoint for enemy.
    /// </summary>
    /// <param name="index">waypoint to follow</param>
    public void SetWaypoint(int index)
    {
        waypointIndex = index - 1;
        GetNextWaypoint();
    }

}

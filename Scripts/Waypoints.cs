using UnityEngine;

/// <summary>
/// Collects all waypoints which enemies follow when going through level.
/// Script must be put on parent GameObject of all waypoints.
/// </summary>
public class Waypoints : MonoBehaviour
{
    public static Transform[] points { get; private set; }

    private void Awake()
    {
        points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
}

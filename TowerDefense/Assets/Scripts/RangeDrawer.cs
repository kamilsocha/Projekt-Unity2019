using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Displays turrets range.
/// </summary>
public class RangeDrawer : MonoBehaviour
{
    public float yOffset = 1f;
    Vector3 positionOffset;

    private void Awake()
    {
        positionOffset = new Vector3(0, yOffset, 0);
    }
    /// <summary>
    /// Displays circle representing range from specified position.
    /// </summary>
    /// <param name="pos">turrets position</param>
    /// <param name="range">turrets range</param>
    public void Show(Vector3 pos, float range)
    {
        gameObject.SetActive(true);
        transform.position = pos + positionOffset;
        transform.localScale = new Vector3(range * 2, 1, range * 2);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}

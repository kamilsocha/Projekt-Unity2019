using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeDrawer : MonoBehaviour
{
    public float yOffset = 1f;
    Vector3 positionOffset;

    private void Awake()
    {
        positionOffset = new Vector3(0, yOffset, 0);
    }

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

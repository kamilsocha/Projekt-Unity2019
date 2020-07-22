using System.Collections;
using UnityEngine;

/// <summary>
/// Controls movement of world canvas object spawned form pool
/// when turret is bought.
/// </summary>
public class FloatingMoney : MonoBehaviour, IPooledObject
{
    public float lifetime = 7;
    public float upForce = 7f;
    public float xAmplitudeForce = .7f;
    public float zAmplitudeForce = .7f;

    Rigidbody rb;
    CanvasGroup canvas;

    private void Awake()
    {
        canvas = GetComponentInChildren<CanvasGroup>();
        rb = GetComponent<Rigidbody>();
    }
    /// <summary>
    /// Method implemented from IPooledObject interface, when object is spawned from pool
    /// it's properties are reset and it's movement starts from beginning.
    /// </summary>
    public void OnObjectSpawn()
    {
        StopAllCoroutines();
        canvas.alpha = 1;
        rb.velocity = new Vector3(0, upForce, 0);
        StartCoroutine(FloatMoney());
    }
    /// <summary>
    /// Controls canvas's movement in x and z axis using trigonometric functions.
    /// </summary>
    /// <returns></returns>
    IEnumerator FloatMoney()
    {
        float t = 0f;
        float startX = transform.position.x;
        float startZ = transform.position.z;
        float curX;
        float curZ;

        while(t < lifetime)
        {
            curX = startX + xAmplitudeForce * Mathf.Sin(t);
            curZ = startZ + zAmplitudeForce * Mathf.Cos(t);
            transform.position = new Vector3(curX, transform.position.y, curZ);
            canvas.alpha = 1 - t / (lifetime);
            t += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
    }

}

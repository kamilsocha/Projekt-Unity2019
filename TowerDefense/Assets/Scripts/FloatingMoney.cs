﻿using System.Collections;
using UnityEngine;

public class FloatingMoney : MonoBehaviour, IPooledObject
{
    public float lifetime = 7;
    public float upForce = 7f;
    public float xAmplitudeForce = .7f;
    public float zAmplitudeForce = .7f;

    CanvasGroup canvas;

    public void OnObjectSpawn()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, upForce, 0);
        canvas = GetComponentInChildren<CanvasGroup>();
        StartCoroutine(FloatMoney());
    }

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

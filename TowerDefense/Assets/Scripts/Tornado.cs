using System.Collections;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    public iTween.EaseType movementEaseType = iTween.EaseType.easeInOutSine;
    public float movementRadius = 30f;
    public float movementSpeed = 0.5f;
    public float timeOffset = 0.1f;
    public string colliderTag = "Object";
    
    public Transform pullingCenter;
    public AnimationCurve pullingCenterCurve;
    public float pullForce;
    public float pullForceMult = 1000;
    public AnimationCurve pullForceCurve;
    public float refreshRate;

    Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
        StartCoroutine(Movement());
    }

    IEnumerator Movement()
    {
        var destination = new Vector3(startPosition.x + Random.Range(-movementRadius, movementRadius), startPosition.y, startPosition.z + Random.Range(-movementRadius, movementRadius));
        var distance = destination - startPosition;
        var time = distance.magnitude / movementSpeed;

        iTween.MoveTo(gameObject, iTween.Hash("position", destination, "easeType", movementEaseType, "time", time));
        yield return new WaitForSeconds(time + timeOffset);
        StartCoroutine(Movement());
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("collider enter");
        if(collider.tag == colliderTag)
        {
            Debug.Log("start coroutine");
            StartCoroutine(PullObject(collider, true));
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if(collider.tag == colliderTag)
        {
            StartCoroutine(PullObject(collider, false));
        }
    }

    IEnumerator PullObject(Collider collider, bool doPull)
    {
        if(doPull)
        {
            pullForce = pullForceMult * pullForceCurve.Evaluate((Time.time * 0.1f) % pullForceCurve.length);
            Debug.Log($"Pullforce: {pullForce}");
            Vector3 forceDirection = pullingCenter.position - collider.transform.position;
            collider.GetComponent<Rigidbody>().AddForce(forceDirection.normalized * pullForce * Time.deltaTime);
            pullingCenter.position = new Vector3(pullingCenter.position.x, pullingCenterCurve.Evaluate(((Time.time * 0.1f) % pullingCenterCurve.length)), pullingCenter.position.z);
            yield return refreshRate;
            StartCoroutine(PullObject(collider, doPull));
        }
        else
        {
            yield return refreshRate;
            StartCoroutine(PullObject(collider, doPull));
        }
    }
}

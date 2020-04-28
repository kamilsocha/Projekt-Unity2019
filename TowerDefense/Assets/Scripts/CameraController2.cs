using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2 : MonoBehaviour
{
    bool doMovement = true;
    public float scrollSpeed = 5;
    public float scrollEdge = 0.1f;
    public float panSpeed = 10;
    public Vector2 zoomRange = new Vector2(-20, 100);
    public float currentZoom = 0;
    public float zoomSpeed = 1;
    public float zoomRotation = 1;
    public Vector2 zoomAngleRange = new Vector2(20, 70);
    public float rotateSpeed = 10;
    private Vector3 initialPosition;
    private Vector3 initialRotation;


    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.eulerAngles;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) doMovement = !doMovement;

        if (!doMovement) return;

        // panning     
        if (Input.GetMouseButton(0))
        {
            transform.Translate(Vector3.right * Time.deltaTime * panSpeed * (Input.mousePosition.x - Screen.width * 0.5f) / (Screen.width * 0.5f), Space.World);
            transform.Translate(Vector3.forward * Time.deltaTime * panSpeed * (Input.mousePosition.y - Screen.height * 0.5f) / (Screen.height * 0.5f), Space.World);
        }

        else
        {
            if (Input.GetKey("d"))
            {
                transform.Translate(Vector3.right * Time.deltaTime * panSpeed, Space.Self);
            }
            else if (Input.GetKey("a"))
            {
                transform.Translate(Vector3.right * Time.deltaTime * -panSpeed, Space.Self);
            }

            if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height * (1 - scrollEdge))
            {
                transform.Translate(Vector3.forward * Time.deltaTime * panSpeed, Space.Self);
            }
            else if (Input.GetKey("s") || Input.mousePosition.y <= Screen.height * scrollEdge)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * -panSpeed, Space.Self);
            }

            if (Input.GetKey("q") || Input.mousePosition.x <= Screen.width * scrollEdge)
            {
                transform.Rotate(Vector3.up * Time.deltaTime * -rotateSpeed, Space.World);
            }
            else if (Input.GetKey("e") || Input.mousePosition.x >= Screen.width * (1 - scrollEdge))
            {
                transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed, Space.World);
            }
        }

        // zoom in/out
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 1000 * zoomSpeed;

        currentZoom = Mathf.Clamp(currentZoom, zoomRange.x, zoomRange.y);

        transform.position = new Vector3(transform.position.x, transform.position.y - (transform.position.y - (initialPosition.y + currentZoom)) * 0.1f, transform.position.z);

        float x = transform.eulerAngles.x - (transform.eulerAngles.x - (initialRotation.x + currentZoom * zoomRotation)) * 0.1f;
        x = Mathf.Clamp(x, zoomAngleRange.x, zoomAngleRange.y);

        transform.eulerAngles = new Vector3(x, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}

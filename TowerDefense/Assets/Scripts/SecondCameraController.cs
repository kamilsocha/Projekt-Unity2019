using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SecondCameraController : MonoBehaviour
{
    public Transform cameraTransform;
    public float movementTime;
    public float rotationAmount;
    public float zoomAmount;
    public float scrollSpeed;

    Quaternion newRotation;
    Vector3 newZoom;

    public float xRotation = 0f;
    public Vector3 newCameraRotation;

    public float minCameraRotation = 1;
    public float maxCameraRotation = 60;

    Camera _camera;
    AudioListener audioListener;

    public float minZoomY;
    public float maxZoomY;

    public bool isActive;
    bool doMovement;

    public Node target;

    private void Start()
    {
        _camera = GetComponentInChildren<Camera>();
        audioListener = GetComponentInChildren<AudioListener>();

        newCameraRotation = cameraTransform.localRotation.eulerAngles;

        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
        doMovement = false;
        isActive = false;
        _camera.enabled = false;
        audioListener.enabled = false;
        FindObjectOfType<NodeUI>().OnCamerasSwitch += HandleCameraSwitch;
        FindObjectOfType<GameManager>().OnLevelWon += HandleLevelWon;
    }

    private void Update()
    {
        if (doMovement)
            HandleMovementInput();
    }

    void HandleMovementInput()
    {
        if (GameManager.GameIsOver)
        {
            this.enabled = false;
            return;
        }

        if (Input.GetKey(KeyCode.W))
        {
            xRotation += rotationAmount;
        }
        if (Input.GetKey(KeyCode.S))
        {
            xRotation -= rotationAmount;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }


        if (Input.GetKey(KeyCode.R) && cameraTransform.localPosition.y >= minZoomY)
        {
            newZoom.y += zoomAmount;
        }
        else if (Input.GetKey(KeyCode.F) && cameraTransform.localPosition.y <= maxZoomY)
        {
            newZoom.y -= zoomAmount;
        }
        else
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            newZoom.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        }

        newZoom.y = Mathf.Clamp(newZoom.y, minZoomY, maxZoomY);

        xRotation = Mathf.Clamp(xRotation, minCameraRotation, maxCameraRotation);
        newCameraRotation = cameraTransform.rotation.eulerAngles;
        newCameraRotation.x = xRotation;
        Vector3 rotation = Quaternion.Lerp(cameraTransform.localRotation, Quaternion.Euler(newCameraRotation), Time.deltaTime * movementTime).eulerAngles;
        cameraTransform.localRotation = Quaternion.Euler(rotation.x, 0f, 0f);


        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
        
    }

    public void SetTarget(Node _target)
    {
        target = _target;
        transform.position = target.GetBuildPosition();
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }

    void HandleCameraSwitch()
    {
        doMovement = !doMovement;
        if (isActive)
        {
            isActive = false;
            _camera.enabled = false;
            audioListener.enabled = false;
            target = null;
        }
        else
        {
            isActive = true;
            _camera.enabled = true;
            audioListener.enabled = true;
        }
    }

    void HandleLevelWon()
    {
        if(isActive)
            HandleCameraSwitch();
    }
}

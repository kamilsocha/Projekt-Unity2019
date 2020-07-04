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

    Camera _camera;
    AudioListener audioListener;

    public float minY;
    public float maxY;

    public bool isActive;
    bool doMovement;

    public Node target;

    private void Start()
    {
        _camera = GetComponentInChildren<Camera>();
        audioListener = GetComponentInChildren<AudioListener>();
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

        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }


        if (Input.GetKey(KeyCode.R) && cameraTransform.localPosition.y >= minY)
        {
            newZoom.y += zoomAmount;
        }
        else if (Input.GetKey(KeyCode.F) && cameraTransform.localPosition.y <= maxY)
        {
            newZoom.y -= zoomAmount;
        }
        else
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            newZoom.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        }

        newZoom.y = Mathf.Clamp(newZoom.y, minY, maxY);

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Strategy like camera. Allows to move in specified boundaries, zoom in and out
/// as well as rotate around the level.
/// </summary>
public class StrategyCamera : MonoBehaviour
{
    public Transform cameraTransform;
    bool doMovement;
    public float normalSpeed;
    public float fastSpeed;
    public float movementSpeed;
    public float movementTime;
    public float rotationAmount;
    public Vector3 zoomAmount;
    public float scrollSpeed;

    Vector3 newPosition;
    Quaternion newRotation;
    Vector3 newZoom;

    string cameraMovementEnableKey;
    public float panSpeed = 30f;
    public float panBoarderThickness = 10f;

    public Vector3 minBorders;
    public Vector3 maxBorders;

    public bool isActive { get; private set; }
    Camera _camera;
    AudioListener audioListener;

    Vector3 initRigPosition;
    Quaternion initRotation;
    Vector3 initCameraPosition;

    CameraUI cameraUI;

    void Start()
    {
        isActive = true;
        doMovement = false;
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
        cameraMovementEnableKey = PlayerPrefs.GetString("cameraMovementEnableKey", "m");
        _camera = GetComponentInChildren<Camera>();
        audioListener = GetComponentInChildren<AudioListener>();
        FindObjectOfType<NodeUI>().OnCamerasSwitch += HandleCamerasSwitch;
        FindObjectOfType<GameManager>().OnLevelWon += HandleLevelWon;

        initRigPosition = transform.position;
        initRotation = transform.rotation;
        initCameraPosition = _camera.transform.localPosition;

        cameraUI = FindObjectOfType<CameraUI>();
    }

    void Update()
    {
        if (isActive)
            HandleMovementInput();
    }

    void HandleMovementInput()
    {
        if (Input.GetKeyDown(cameraMovementEnableKey))
        {
            if(doMovement)
            {
                cameraUI.ToggleCameraText(false);
                doMovement = false;
            } else
            {
                cameraUI.ToggleCameraText(true);
                doMovement = true;
            }
        }

        if (!doMovement) return;

        if(Input.GetKey(KeyCode.LeftShift)) 
        {
            movementSpeed = fastSpeed;
        } 
        else
        {
            movementSpeed = normalSpeed;
        }

        if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y >= Screen.height - panBoarderThickness)
        {
            newPosition += (transform.forward * movementSpeed);
        }
        else if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= panBoarderThickness)
        {
            newPosition += (transform.forward * -movementSpeed);
        }
        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width - panBoarderThickness)
        {
            newPosition += (transform.right * movementSpeed);
        }
        else if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= panBoarderThickness)
        {
            newPosition += (transform.right * -movementSpeed);
        }

        if(Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }

        
        if(Input.GetKey(KeyCode.R) && cameraTransform.localPosition.y >= minBorders.y)
        {
            newZoom += zoomAmount;
        }
        else if(Input.GetKey(KeyCode.F) && cameraTransform.localPosition.y <= maxBorders.y)
        {
            newZoom -= zoomAmount;
        }
        else
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            newZoom.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        }

        newPosition.x = Mathf.Clamp(newPosition.x, minBorders.x, maxBorders.x);
        newZoom.y = Mathf.Clamp(newZoom.y, minBorders.y, maxBorders.y);
        newZoom.z = Mathf.Clamp(newZoom.z, minBorders.z, maxBorders.z);
        newPosition.z = Mathf.Clamp(newPosition.z, minBorders.z, maxBorders.z);


        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }
    /// <summary>
    /// Controls if camera should be active. When deactivated restores its' original position.
    /// </summary>
    void HandleCamerasSwitch()
    {
        if (isActive)
        {
            isActive = false;
            _camera.enabled = false;
            audioListener.enabled = false;
            doMovement = false;
            cameraUI.Hide();

            newPosition.x = initRigPosition.x;
            newZoom.y = initCameraPosition.y;
            newZoom.z = initCameraPosition.z;
            newPosition.z = initRigPosition.z;

            transform.position = initRigPosition;
            transform.rotation = initRotation;
            cameraTransform.localPosition = initCameraPosition;
        }
        else
        {
            isActive = true;
            _camera.enabled = true;
            audioListener.enabled = true;
            doMovement = true;
            cameraUI.Show();
        }
    }

    /// <summary>
    /// When level is won it returns to original position.
    /// </summary>
    void HandleLevelWon()
    {
        newPosition = initRigPosition;
        newRotation = initRotation;
        newZoom = initCameraPosition;
        isActive = true;
    }

}

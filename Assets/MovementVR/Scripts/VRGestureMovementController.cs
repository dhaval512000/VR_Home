using UnityEngine;

public class VRGestureMovementController : MonoBehaviour
{
    [Header("Gesture Detectors")]
    public GenericHandGestureDetector leftForwardDetector;
    public GenericHandGestureDetector rightForwardDetector;
    public GenericHandGestureDetector leftSidewaysFrontDetector;
    public GenericHandGestureDetector leftSidewaysBackDetector;
    public GenericHandGestureDetector rightSidewaysFrontDetector;
    public GenericHandGestureDetector rightSidewaysBackDetector;

    [Header("Movement Settings")]
    public float moveSpeed = 1.0f;
    public float rotationAngle = 30.0f;
    public float rotationInterval = 0.5f;

    [Header("References")]
    public Transform cameraTransform;
    public CharacterController characterController;
    public Transform movementTargets;
    public float heightOffsetForPoints = 0.5f;

    private bool isLeftForwardDetected = false;
    private bool isRightForwardDetected = false;
    private bool isLeftSidewaysFrontDetected = false;
    private bool isLeftSidewaysBackDetected = false;
    private bool isRightSidewaysFrontDetected = false;
    private bool isRightSidewaysBackDetected = false;

    private float lastLeftRotationTime = 0f;
    private float lastRightRotationTime = 0f;

    private Quaternion targetRotation;

    private void Awake()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        targetRotation = transform.rotation;
    }

    private void Start()
    {
        Vector3 movementPos = movementTargets.transform.position;
        movementPos.y = cameraTransform.position.y - heightOffsetForPoints;
        movementTargets.transform.position = movementPos;
    }

    private void OnEnable()
    {
        SubscribeToGestureEvents();
    }

    private void OnDisable()
    {
        UnsubscribeFromGestureEvents();
    }

    private void SubscribeToGestureEvents()
    {
        if (leftForwardDetector != null)
            leftForwardDetector.onGestureChanged.AddListener(OnLeftForwardGestureChanged);
        if (rightForwardDetector != null)
            rightForwardDetector.onGestureChanged.AddListener(OnRightForwardGestureChanged);
        if (leftSidewaysFrontDetector != null)
            leftSidewaysFrontDetector.onGestureChanged.AddListener(OnLeftSidewaysFrontGestureChanged);
        if (leftSidewaysBackDetector != null)
            leftSidewaysBackDetector.onGestureChanged.AddListener(OnLeftSidewaysBackGestureChanged);
        if (rightSidewaysFrontDetector != null)
            rightSidewaysFrontDetector.onGestureChanged.AddListener(OnRightSidewaysFrontGestureChanged);
        if (rightSidewaysBackDetector != null)
            rightSidewaysBackDetector.onGestureChanged.AddListener(OnRightSidewaysBackGestureChanged);
    }

    private void UnsubscribeFromGestureEvents()
    {
        if (leftForwardDetector != null)
            leftForwardDetector.onGestureChanged.RemoveListener(OnLeftForwardGestureChanged);
        if (rightForwardDetector != null)
            rightForwardDetector.onGestureChanged.RemoveListener(OnRightForwardGestureChanged);
        if (leftSidewaysFrontDetector != null)
            leftSidewaysFrontDetector.onGestureChanged.RemoveListener(OnLeftSidewaysFrontGestureChanged);
        if (leftSidewaysBackDetector != null)
            leftSidewaysBackDetector.onGestureChanged.RemoveListener(OnLeftSidewaysBackGestureChanged);
        if (rightSidewaysFrontDetector != null)
            rightSidewaysFrontDetector.onGestureChanged.RemoveListener(OnRightSidewaysFrontGestureChanged);
        if (rightSidewaysBackDetector != null)
            rightSidewaysBackDetector.onGestureChanged.RemoveListener(OnRightSidewaysBackGestureChanged);
    }

    private void OnLeftForwardGestureChanged(bool detected) => isLeftForwardDetected = detected;
    private void OnRightForwardGestureChanged(bool detected) => isRightForwardDetected = detected;
    private void OnLeftSidewaysFrontGestureChanged(bool detected) => isLeftSidewaysFrontDetected = detected;
    private void OnLeftSidewaysBackGestureChanged(bool detected) => isLeftSidewaysBackDetected = detected;
    private void OnRightSidewaysFrontGestureChanged(bool detected) => isRightSidewaysFrontDetected = detected;
    private void OnRightSidewaysBackGestureChanged(bool detected) => isRightSidewaysBackDetected = detected;

    private void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        if (isLeftForwardDetected || isRightForwardDetected)
        {
            Vector3 forward = cameraTransform.forward;
            forward.y = 0; // Ensure movement is on the horizontal plane
            forward.Normalize();

            Vector3 movement = forward * moveSpeed * Time.deltaTime;
            //xrRig.position += movement;
            characterController.Move(movement);
        }
    }

    //private void HandleRotation()
    //{
    //    // Left hand rotation
    //    if (Time.time - lastLeftRotationTime >= rotationInterval)
    //    {
    //        if (isLeftSidewaysFrontDetected)
    //        {
    //            characterController.transform.Rotate(Vector3.up, rotationAngle); // Rotate right
    //            lastLeftRotationTime = Time.time;
    //        }
    //        else if (isLeftSidewaysBackDetected)
    //        {
    //            characterController.transform.Rotate(Vector3.up, -rotationAngle); // Rotate left
    //            lastLeftRotationTime = Time.time;
    //        }
    //    }

    //    // Right hand rotation
    //    if (Time.time - lastRightRotationTime >= rotationInterval)
    //    {
    //        if (isRightSidewaysFrontDetected)
    //        {
    //            characterController.transform.Rotate(Vector3.up, -rotationAngle); // Rotate left
    //            lastRightRotationTime = Time.time;
    //        }
    //        else if (isRightSidewaysBackDetected)
    //        {
    //            characterController.transform.Rotate(Vector3.up, rotationAngle); // Rotate right
    //            lastRightRotationTime = Time.time;
    //        }
    //    }
    //}

    private void HandleRotation()
    {
        float rotationAmount = 0f;

        // Left hand rotation
        if (isLeftSidewaysFrontDetected)
            rotationAmount = rotationAngle;
        else if (isLeftSidewaysBackDetected)
            rotationAmount = -rotationAngle;

        // Right hand rotation
        if (isRightSidewaysFrontDetected)
            rotationAmount = -rotationAngle;
        else if (isRightSidewaysBackDetected)
            rotationAmount = rotationAngle;

        if (rotationAmount != 0f)
        {
            characterController.transform.Rotate(Vector3.up, rotationAmount * Time.deltaTime); // Rotate right
        }
    }
}
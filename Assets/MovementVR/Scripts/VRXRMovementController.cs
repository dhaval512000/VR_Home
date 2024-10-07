using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRXRMovementController : MonoBehaviour
{
    [Header("XR Simple Interactables")]
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable leftInteractable;
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable rightInteractable;
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable forwardInteractable;

    [Header("Movement Settings")]
    public float moveSpeed = 1.0f;
    public float rotationAngle = 30.0f;

    [Header("References")]
    public Transform cameraTransform;
    public CharacterController characterController;

    private bool isMovingForward = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;

    private void Awake()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;
    }

    private void OnEnable()
    {
        SubscribeToInteractableEvents();
    }

    private void OnDisable()
    {
        UnsubscribeFromInteractableEvents();
    }

    private void SubscribeToInteractableEvents()
    {
        if (leftInteractable != null)
        {
            leftInteractable.selectEntered.AddListener(OnLeftSelectEntered);
            leftInteractable.selectExited.AddListener(OnLeftSelectExited);
        }

        if (rightInteractable != null)
        {
            rightInteractable.selectEntered.AddListener(OnRightSelectEntered);
            rightInteractable.selectExited.AddListener(OnRightSelectExited);
        }

        if (forwardInteractable != null)
        {
            forwardInteractable.selectEntered.AddListener(OnForwardSelectEntered);
            forwardInteractable.selectExited.AddListener(OnForwardSelectExited);
        }
    }

    private void UnsubscribeFromInteractableEvents()
    {
        if (leftInteractable != null)
        {
            leftInteractable.selectEntered.RemoveListener(OnLeftSelectEntered);
            leftInteractable.selectExited.RemoveListener(OnLeftSelectExited);
        }

        if (rightInteractable != null)
        {
            rightInteractable.selectEntered.RemoveListener(OnRightSelectEntered);
            rightInteractable.selectExited.RemoveListener(OnRightSelectExited);
        }

        if (forwardInteractable != null)
        {
            forwardInteractable.selectEntered.RemoveListener(OnForwardSelectEntered);
            forwardInteractable.selectExited.RemoveListener(OnForwardSelectExited);
        }
    }

    private void OnLeftSelectEntered(SelectEnterEventArgs args) => isRotatingLeft = true;
    private void OnLeftSelectExited(SelectExitEventArgs args) => isRotatingLeft = false;
    private void OnRightSelectEntered(SelectEnterEventArgs args) => isRotatingRight = true;
    private void OnRightSelectExited(SelectExitEventArgs args) => isRotatingRight = false;
    private void OnForwardSelectEntered(SelectEnterEventArgs args) => isMovingForward = true;
    private void OnForwardSelectExited(SelectExitEventArgs args) => isMovingForward = false;

    private void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        if (isMovingForward)
        {
            Vector3 forward = cameraTransform.forward;
            forward.y = 0; // Ensure movement is on the horizontal plane
            forward.Normalize();

            Vector3 movement = forward * moveSpeed * Time.deltaTime;
            characterController.Move(movement);
        }
    }

    private void HandleRotation()
    {
        float rotationAmount = 0f;

        if (isRotatingLeft)
            rotationAmount = -rotationAngle;
        else if (isRotatingRight)
            rotationAmount = rotationAngle;

        if (rotationAmount != 0f)
        {
            characterController.transform.Rotate(Vector3.up, rotationAmount * Time.deltaTime);
        }
    }
}
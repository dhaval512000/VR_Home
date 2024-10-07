using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Hands;
using UnityEngine.XR.Hands.Gestures;

public class GenericHandGestureDetector : MonoBehaviour
{
    [System.Serializable]
    public class GestureEvent : UnityEvent<bool> { }

    [SerializeField]
    private XRHandTrackingEvents handTrackingEvents;

    [SerializeField]
    private ScriptableObject gestureShapeOrPose;

    [SerializeField]
    private Transform targetTransform;

    [SerializeField]
    private string gestureName = "Unnamed Gesture";

    [SerializeField]
    private float minimumHoldTime = 0.2f;

    [SerializeField]
    private float gestureDetectionInterval = 0.1f;

    public GestureEvent onGestureChanged = new GestureEvent();

    private bool isGestureDetected = false;
    private bool wasGestureDetected = false;
    private float gestureHoldStartTime;
    private float lastDetectionTime;

    private XRHandShape gestureShape;
    private XRHandPose gesturePose;


    public XRHandTrackingEvents HandTrackingEvents
    {
        get => handTrackingEvents;
        set => handTrackingEvents = value;
    }

    public XRHandShape GestureShape
    {
        get => gestureShape;
        set => gestureShape = value;
    }

    public string GestureName
    {
        get => gestureName;
        set => gestureName = value;
    }

    public float MinimumHoldTime
    {
        get => minimumHoldTime;
        set => minimumHoldTime = value;
    }

    public float GestureDetectionInterval
    {
        get => gestureDetectionInterval;
        set => gestureDetectionInterval = value;
    }

    private void OnEnable()
    {
        gestureShape = gestureShapeOrPose as XRHandShape;
        gesturePose = gestureShapeOrPose as XRHandPose;

        if (handTrackingEvents != null)
        {
            handTrackingEvents.jointsUpdated.AddListener(OnJointsUpdated);
        }

        if (gesturePose != null && gesturePose.relativeOrientation != null)
            gesturePose.relativeOrientation.targetTransform = targetTransform;
    }

    private void OnDisable()
    {
        if (handTrackingEvents != null)
        {
            handTrackingEvents.jointsUpdated.RemoveListener(OnJointsUpdated);
        }
    }

    private void OnJointsUpdated(XRHandJointsUpdatedEventArgs eventArgs)
    {
        if (!handTrackingEvents.handIsTracked)
        {
            ResetGesture();
            return;
        }

        if (Time.time < lastDetectionTime + gestureDetectionInterval)
            return;

        lastDetectionTime = Time.time;
        CheckGesture(eventArgs);
    }

    private void CheckGesture(XRHandJointsUpdatedEventArgs eventArgs)
    {
        bool currentlyDetected = (gestureShape != null && gestureShape.CheckConditions(eventArgs)) || (gesturePose != null && gesturePose.CheckConditions(eventArgs));

        if (currentlyDetected && !wasGestureDetected)
        {
            gestureHoldStartTime = Time.time;
        }
        else if (!currentlyDetected && wasGestureDetected)
        {
            if (isGestureDetected)
            {
                isGestureDetected = false;
                onGestureChanged.Invoke(false);
                Debug.Log($"{gestureName} gesture ended");
            }
        }
        else if (currentlyDetected && !isGestureDetected)
        {
            if (Time.time - gestureHoldStartTime >= minimumHoldTime)
            {
                isGestureDetected = true;
                onGestureChanged.Invoke(true);
                Debug.Log($"{gestureName} gesture detected");
            }
        }

        wasGestureDetected = currentlyDetected;
    }

    private void ResetGesture()
    {
        if (isGestureDetected)
        {
            isGestureDetected = false;
            wasGestureDetected = false;
            onGestureChanged.Invoke(false);
            Debug.Log($"{gestureName} gesture ended (hand tracking lost)");
        }
    }
}
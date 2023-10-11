using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;
    public TextureSelectionUiPanel textureSelectionUiPanel;
    public ScreenShotUiPanel screenShotUiPanel;
    public TextureResetPopup textureResetPopup;
    public float uiOffset = 0.5f;
    private void Awake()
    {
        Instance = this;
    }
    
    public void SetCanvasPosition()
    {
        Vector3 uiDirection = Camera.main.transform.forward;
        uiDirection.y = 0;
        Vector3 newPosition = Camera.main.transform.position + uiDirection * uiOffset;
        //transform.position = Camera.main.transform.position + offset; 
        //transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * alignmentSpeed);
        transform.position = newPosition;
        Vector3 direction = transform.position - Camera.main.transform.position;
        Vector3 lookAtPoint = transform.position + direction;

        transform.LookAt(lookAtPoint);
    }
}

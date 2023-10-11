using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TextureResetPopup : MonoBehaviour
{
    public Canvas uiCanvas;
    public InputActionReference resetTextureInput;
    public void ResetAllTextures()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.uiAudioSource,SoundManager.Instance.buttonClick);
        Events.ResetAllTextures();
        Hide();
    }

    private void OnEnable()
    {
        resetTextureInput.action.performed += ShowTextureResetPopup;
    }

    private void OnDisable()
    {
        resetTextureInput.action.performed -= ShowTextureResetPopup;
    }

    public void ShowTextureResetPopup(InputAction.CallbackContext obj)
    {
        Show();
    }

    [ContextMenu("reset popup")]
    private void Show()
    {
        UiManager.Instance.SetCanvasPosition();
        UiManager.Instance.textureSelectionUiPanel.Hide();
        UiManager.Instance.screenShotUiPanel.Hide();
        uiCanvas.enabled = true;
    }
    
    public void Hide()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.uiAudioSource,SoundManager.Instance.buttonClick);
        uiCanvas.enabled = false;
    }
}

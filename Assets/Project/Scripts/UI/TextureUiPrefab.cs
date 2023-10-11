using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureUiPrefab : MonoBehaviour
{
    public int index;
    //public RawImage rawImage;
    public Image previewImageUi;
    public GameObject outlineImage;

    public void OnButtonClick()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.uiAudioSource,SoundManager.Instance.buttonClick);
        UiManager.Instance.textureSelectionUiPanel.ChangeCurrentTexture(index);
    }

    public void EnableDisableHighlightImage(bool imageStatus)
    {
        outlineImage.SetActive(imageStatus);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextureSelectionUiPanel : MonoBehaviour
{
    //public static TextureSelectionUiPanel Instance;
    public Canvas uiCanvas;
    public ObjectBehaviourHandler currentSelectedObjectReference;
    public TextureUiPrefab uiPrefabReference;
    public RectTransform rootTransform;
    public List<TextureUiPrefab> uiPrefabList;
    public int currentUiTextureId;
    public TMP_Text uiObjectName;
    public Scrollbar scrollbar;

    private void Awake()
    {
        //Instance = this;
    }

    public void GenerateTextureListUi(List<TextureData> textureList, int currentTextureId, ObjectBehaviourHandler objectReference)
    {
        Show();
        scrollbar.value = 0;
        currentSelectedObjectReference = objectReference;
        
        uiObjectName.text = currentSelectedObjectReference.objectName;
        
        foreach (Transform t in rootTransform)
        {
            Destroy(t.gameObject);
        }
        uiPrefabList.Clear();
        
        for (int i = 0; i < textureList.Count; i++)
        {
            TextureUiPrefab texturePrefab = Instantiate(uiPrefabReference,rootTransform);
            texturePrefab.index = i;
            texturePrefab.previewImageUi.sprite = textureList[i].previewImageData;
            uiPrefabList.Add(texturePrefab);
        }
        uiPrefabList[currentTextureId].EnableDisableHighlightImage(true);
        currentUiTextureId = currentTextureId;
        Debug.Log("scroll bar"+scrollbar.value);
        Show();
    }

    public void ChangeCurrentTexture(int selectedTexture)
    {
        currentSelectedObjectReference.ChangeMaterialTexture(selectedTexture);
        uiPrefabList[currentUiTextureId].EnableDisableHighlightImage(false);
        currentUiTextureId = selectedTexture;
        uiPrefabList[currentUiTextureId].EnableDisableHighlightImage(true);
    }

    public void Show()
    {
        scrollbar.value = 0;
        UiManager.Instance.SetCanvasPosition();
        UiManager.Instance.textureResetPopup.Hide();
        UiManager.Instance.screenShotUiPanel.Hide();
        uiCanvas.enabled = true;
        scrollbar.value = 0;
        Debug.Log("scroll bar"+scrollbar.value);
    }
    
    public void Hide()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.uiAudioSource,SoundManager.Instance.buttonClick);
        uiCanvas.enabled = false;
    }
}

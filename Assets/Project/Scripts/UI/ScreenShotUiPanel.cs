using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenShotUiPanel : MonoBehaviour
{
    public Canvas uiCanvas;
    public ScreenShotUiPrefab uiPrefabReference;
    public RectTransform rootTransform;
    public List<ScreenShotUiPrefab> uiPrefabList;
    public Scrollbar scrollbar;

    public void GenerateScreenShotListUi(List<Texture2D> textureList)
    {
        foreach (Transform t in rootTransform)
        {
            Destroy(t.gameObject);
        }
        uiPrefabList.Clear();
        
        for (int i = 0; i < textureList.Count; i++)
        {
            ScreenShotUiPrefab texturePrefab = Instantiate(uiPrefabReference,rootTransform);
            texturePrefab.rawImage.texture = textureList[i];
            uiPrefabList.Add(texturePrefab);
        }
        Show();
    }
    
    private void Show()
    {
        UiManager.Instance.SetCanvasPosition();
        UiManager.Instance.textureResetPopup.Hide();
        UiManager.Instance.textureSelectionUiPanel.Hide();
        uiCanvas.enabled = true;
        scrollbar.value = 0;
    }
    
    public void Hide()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.uiAudioSource,SoundManager.Instance.buttonClick);
        uiCanvas.enabled = false;
    }
}

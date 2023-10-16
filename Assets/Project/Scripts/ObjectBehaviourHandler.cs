using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectBehaviourHandler : MonoBehaviour
{
    public TextureDataSO textureSO;
    public Material objectMaterial;
    public int currentSelectedTextureId;
    public string objectName;
    //public bool isDetected;
    //[ColorUsage(true, true)]
    public Color EnableColor;
    //[ColorUsage(true, true)]
    public Color DisableColor;

    private void Start()
    {
        SetDefaultTexture();
    }

    private void OnEnable()
    {
        Events.onTextureReset += SetDefaultTexture;
        GetComponent<XRSimpleInteractable>().selectEntered.AddListener(OnObjectDetect);
        GetComponent<XRSimpleInteractable>().hoverEntered.AddListener(EnableEmission);
        GetComponent<XRSimpleInteractable>().hoverExited.AddListener(DisableEmission);
    }

    private void OnDisable()
    {
        Events.onTextureReset -= SetDefaultTexture;
        GetComponent<XRSimpleInteractable>().selectEntered.RemoveListener(OnObjectDetect);
        GetComponent<XRSimpleInteractable>().hoverEntered.RemoveListener(EnableEmission);
        GetComponent<XRSimpleInteractable>().hoverExited.RemoveListener(DisableEmission);
    }
    public void EnableEmission(HoverEnterEventArgs args)
    {
        Debug.Log("EnableEmission_"+gameObject.name);
        //objectMaterial.EnableKeyword("_EMISSION");
        objectMaterial.SetColor("_BaseColor",EnableColor);
    }
    
    public void DisableEmission(HoverExitEventArgs args)
    {
        Debug.Log("DisableEmission_"+gameObject.name);
        //objectMaterial.DisableKeyword("_EMISSION");
        objectMaterial.SetColor("_BaseColor",DisableColor);
    }

    private void SetDefaultTexture()
    {
        ChangeMaterialTexture(0);
    }
    
    public void OnObjectDetect(SelectEnterEventArgs args)
    {
        Detect();
        //objectMaterial.DisableKeyword("_EMISSION");
        objectMaterial.SetColor("_BaseColor",DisableColor);
    }

    [ContextMenu("On Object Detect")]
    public void Detect()
    {
        Debug.Log("object detected");
        SoundManager.Instance.PlaySound(SoundManager.Instance.uiAudioSource,SoundManager.Instance.buttonClick);
        UiManager.Instance.textureSelectionUiPanel.GenerateTextureListUi(textureSO.textureDataList,currentSelectedTextureId,this);
    }

    public void ChangeMaterialTexture(int textureId)
    {
        TextureData newTextureData = textureSO.textureDataList[textureId];
        objectMaterial.mainTexture = newTextureData.texture;
        objectMaterial.mainTextureScale = newTextureData.tilingValue;
        currentSelectedTextureId = textureId;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data",menuName = "ScriptableObjects/Texture_DataSO")]
public class TextureDataSO : ScriptableObject
{
    public List<TextureData> textureDataList;
}

[System.Serializable]
public class TextureData
{
    public Texture texture;
    public Vector2Int tilingValue;
    public Sprite previewImageData;
}

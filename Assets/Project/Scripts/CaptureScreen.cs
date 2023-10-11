using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using TMPro;
using System.IO;

namespace VRSelfie
{
    public class CaptureScreen : MonoBehaviour
    {
        public Camera selfieCamera;
        //public TextMeshProUGUI timerText;
        public RawImage photoShow;
        bool isReadyToTakeSelfie = true;
        private Texture2D capturedSelfieTexture;
        public List<Texture2D> screenshotList;
        
        private void Start()
        {
            capturedSelfieTexture = new Texture2D(Screen.width, Screen.height);
        }

[ContextMenu("tacke")]
        public void Tack()
        {
            StartCoroutine(TakeSelfie());
        }
        
        private IEnumerator TakeSelfie()
        {
            yield return null;
            CaptureSelfie();
        }

        private void CaptureSelfie()
        {
            string filename = "my_selfie.png";//$"my_selfie_{DateTime.Now:yyyyMMddHHmmss}.png";

            RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
            selfieCamera.targetTexture = rt;
            selfieCamera.Render();
            RenderTexture.active = rt;

            capturedSelfieTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            capturedSelfieTexture.Apply();

            // Save image in oculus gallery
            SaveImageToGallery(capturedSelfieTexture, filename);

            photoShow.texture = capturedSelfieTexture;
            isReadyToTakeSelfie = true;
        }

        public string saveFolderPath = "Assets/MyImages";
        private void SaveImageToGallery(Texture2D imageToSave, string filename)
        {
            screenshotList.Add(imageToSave);
            
            if (!Directory.Exists(saveFolderPath))
            {
                Directory.CreateDirectory(saveFolderPath);
            }
            
            string fullPath = Path.Combine(saveFolderPath, filename);

            byte[] bytes = imageToSave.EncodeToPNG();
            File.WriteAllBytes(fullPath, bytes);
        }
    }
}

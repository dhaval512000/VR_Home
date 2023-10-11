using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using TMPro;
using System.IO;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace VRSelfie
{
    public class ScreenShotManager : MonoBehaviour
    {
        public Camera selfieCamera;

        //public TextMeshProUGUI timerText;
        //public RawImage photoShow;
        public RenderTexture renderTexture;
        //bool isReadyToTakeSelfie = true;
        public Texture2D capturedSelfieTexture;
        public List<Texture2D> screenshotList;
        public List<ScreenShotTransforms> screenShotData;
        public Vector2Int resolution;
        public InputActionReference takeScreenshotInput;

        private void Start()
        {
            selfieCamera.gameObject.SetActive(false);
            capturedSelfieTexture = new Texture2D(resolution.x, resolution.y);
        }

        private void OnEnable()
        {
            takeScreenshotInput.action.performed += StartTakingScreenShot;
        }

        private void OnDisable()
        {
            takeScreenshotInput.action.performed += StartTakingScreenShot;
        }

        public void StartTakingScreenShot(InputAction.CallbackContext obj)
        {
            Tack();
        }

        [ContextMenu("tack")]
        public void Tack()
        {
            StartCoroutine(TakeSelfie());
        }

        private IEnumerator TakeSelfie()
        {
            yield return null;
            screenshotList.Clear();
            CaptureSelfie();
        }

        private void CaptureSelfie()
        {
            selfieCamera.gameObject.SetActive(true);
            selfieCamera.targetTexture = renderTexture;
            //RenderTexture rt = new RenderTexture(resolution.x, resolution.y, 16);
            for (int i = 0; i < screenShotData.Count; i++)
            {
                selfieCamera.transform.SetParent(screenShotData[i].screenShotTransformParent);
                selfieCamera.transform.localPosition = Vector3.zero;
                selfieCamera.transform.localEulerAngles = Vector3.zero;
                selfieCamera.fieldOfView = screenShotData[i].fieldOfView;

                string filename = "my_selfie_" + i + ".png"; //$"my_selfie_{DateTime.Now:yyyyMMddHHmmss}.png";

                //RenderTexture rt = new RenderTexture(resolution.x, resolution.y, 24);
                //rt.Release();
                //selfieCamera.targetTexture = renderTexture;
                selfieCamera.Render();
                RenderTexture.active = renderTexture;

                //capturedSelfieTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
                capturedSelfieTexture.ReadPixels(new Rect(0, 0, resolution.x, resolution.y), 0, 0);
                capturedSelfieTexture.Apply();

                // Save image in oculus gallery
                //SaveImageToGallery(capturedSelfieTexture, filename);

                //photoShow.texture = capturedSelfieTexture;

                Texture2D selfie =
                    new Texture2D(capturedSelfieTexture.width,
                        capturedSelfieTexture.height); // = new Texture(capturedSelfieTexture);

                Graphics.CopyTexture(capturedSelfieTexture, selfie);

                screenshotList.Add(selfie);
                //isReadyToTakeSelfie = true;
            }
            UiManager.Instance.screenShotUiPanel.GenerateScreenShotListUi(screenshotList);
            selfieCamera.gameObject.SetActive(false);
            selfieCamera.targetTexture = null;
            //rt.Release();
        }

        public string saveFolderPath = "Assets/MyImages";

        /*private void SaveImageToGallery(Texture2D imageToSave, string filename)
        {
            //screenshotList.Add(imageToSave);

            if (!Directory.Exists(saveFolderPath))
            {
                Directory.CreateDirectory(saveFolderPath);
            }

            string fullPath = Path.Combine(saveFolderPath, filename);

            byte[] bytes = imageToSave.EncodeToPNG();
            File.WriteAllBytes(fullPath, bytes);
        }*/
    }
}

[Serializable]
public class ScreenShotTransforms
{
    public Transform screenShotTransformParent;
    public float fieldOfView;
}
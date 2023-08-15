using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.WebCam;

public class WebcamTextureMgr : MonoBehaviour
{
    [DllImport("OpenCVDLL")]
    private static extern void ProcessImage(ref Color32[] rawImage, int width, int height);
    private WebCamTexture webCam;
    private Texture2D cameraTexture;
    [SerializeField] private Image cameraImage;
    [SerializeField] Button button;

    private void OnEnable()
    {
        button.onClick.AddListener(() =>
        {
            InitWebcam();
        });
    }

    private void Update()
    {
        if (!webCam || !webCam.isPlaying) return; 
        DisplayProcessedImage();
    }

    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }

    private void InitWebcam()
    {
        webCam = new WebCamTexture();
        webCam.Play();
        cameraTexture = new Texture2D(webCam.width, webCam.height);
        cameraImage.material.mainTexture = cameraTexture;
        cameraImage.SetMaterialDirty();
    }

    private void DisplayProcessedImage()
    {
        var rawImage = webCam.GetPixels32();
        ProcessImage(ref rawImage, webCam.width, webCam.height);
        cameraTexture.SetPixels32(rawImage);
        cameraTexture.Apply();
        cameraImage.material.mainTexture = cameraTexture;
    }
}

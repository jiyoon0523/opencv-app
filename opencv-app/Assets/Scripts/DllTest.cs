using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;


public class DllTest : MonoBehaviour
{
    [SerializeField] private Image image;
    private Texture2D texture;
    [SerializeField] private Button flipBtn;

    [DllImport("OpenCVDLL")]
    private static extern void FlipImage(ref Color32[] rawImage, int width, int height);

    [DllImport("OpenCVDLL")]
    private static extern void CaptureVideo();

    private void Awake()
    {
        //texture = image.sprite.texture;

        flipBtn.onClick.AddListener(() =>
        {
            CallCaptureVideo();
        });
    }

    private void CallCaptureVideo()
    {
        Debug.Log($">>> CallCaptureVideo");
        CaptureVideo();
    }

    private void CallFlipImage(Texture2D texture)
    {
        var newTexture = texture.GetPixels32();
        FlipImage(ref newTexture, 400, 400);
        texture.SetPixels32(newTexture);
        texture.Apply();
    }

    private void Convert(Texture2D texture)
    {
        var convertedTexture = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);
        convertedTexture.ReadPixels(new Rect(0, 0, texture.width, texture.height), 0, 0);
        convertedTexture.Apply();
    }
}
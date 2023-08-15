using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class TextureTest : MonoBehaviour
{
    [DllImport("OpenCVDLL")]
    private static extern void GetRawImageBytes(IntPtr data, int width, int height);

    [SerializeField] private RawImage rawImage;
    private Texture2D texture;
    private Color32[] pixel32;

    private GCHandle pixelHandle;
    private IntPtr pixelPtr;

    private void Start()
    {
        InitTexture();
    }

    private void Update()
    {
        SetTexture(MatToTexture2D());
    }

    private void InitTexture()
    {
        texture = new Texture2D(512, 512, TextureFormat.ARGB32, false);
        pixel32 = texture.GetPixels32();
        pixelHandle = GCHandle.Alloc(pixel32, GCHandleType.Pinned);
        pixelPtr = pixelHandle.AddrOfPinnedObject();
    }

    //private void MatToTexture2D()
    //{
    //    GetRawImageBytes(pixelPtr, texture.width, texture.height);
    //    texture.SetPixels32(pixel32);
    //    texture.Apply();
    //}

    private Texture2D MatToTexture2D()
    {
        if (texture == null || texture.width == 0 || texture.height == 0)
        {
            Debug.Log("texture is null or is of size 0");
            InitTexture();
        }

        GetRawImageBytes(pixelPtr, texture.width, texture.height);
        texture.SetPixels32(pixel32);
        texture.Apply();
        return texture;
    }

    private void SetTexture(Texture2D incomingTexture)
    {
        rawImage.texture = incomingTexture;
    }


    private void OnApplicationQuit()
    {
        pixelHandle.Free();
    }


}

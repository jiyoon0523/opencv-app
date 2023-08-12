using AOT;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class DebugCPP : MonoBehaviour
{
    [DllImport("OpenCVDLL", CallingConvention= CallingConvention.StdCall)]
    //[DllImport("OpenCVDLL")]
    static extern void RegisterDebugCallback(debugCallback cb);
    [DllImport("OpenCVDLL", CallingConvention = CallingConvention.StdCall)]
    //[DllImport("OpenCVDLL", CallingConvention = CallingConvention.Cdecl)]
    //[DllImport("OpenCVDLL")]

    static extern void RemoveDebugCallback();

    delegate void debugCallback(IntPtr request, int color, int size);
    enum Color { red, green, blue, black, white, yellow, orange };

    private void OnEnable()
    {
        RegisterDebugCallback(OnDebugCallback);
    }

    private void OnDisable()
    {
        RemoveDebugCallback();
    }

    //[MonoPInvokeCallback(typeof(debugCallback))]
    static void OnDebugCallback(IntPtr request, int color, int size)
    {
        string str = Marshal.PtrToStringAnsi(request, size);
        str =
            String.Format("{0}{1}{2}{3}{4}",
            "<color=",
            ((Color)color).ToString(),
            ">",
            str,
            "</color>"
            );
        Debug.Log(str);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class CameraCheck : MonoBehaviour
{

    void Start()
    {
        Display[] displays = Display.displays;

        for (int i = 0; i < displays.Length; i++)
        {
            Debug.Log($"Display {i + 1} - Native Resolution: {displays[i].systemWidth}x{displays[i].systemHeight}");
        }
    }
}

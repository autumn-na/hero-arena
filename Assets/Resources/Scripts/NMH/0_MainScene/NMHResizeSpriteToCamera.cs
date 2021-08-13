using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHResizeSpriteToCamera : MonoBehaviour
{
    void Awake()
    {
        ResizeSprite();
    }

    void ResizeSprite()
    {
        Screen.orientation = ScreenOrientation.Landscape;

        Camera.main.orthographicSize = 1080 / 2;

        Screen.SetResolution(1920, 1080, true);
    }
}

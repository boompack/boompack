using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenResizer : Singleton<ScreenResizer>
{
    public int height;
    public int width;

    public Image background;


    public int borderSize;

    void Start()
    {
        ResizeCamera();
    }

    public void ResizeCamera()
    {
        Camera.main.transform.position = new Vector3((float)(width -1)/2f, (float)(height -1)/2f,-10f);
        float aspectRatio = (float)Screen.width / (float)Screen.height;
        float verticalSize = height / 2f + borderSize;
        float horizontalSize = (width / 2f + borderSize) / aspectRatio;
        Camera.main.orthographicSize = (verticalSize > horizontalSize) ? verticalSize : horizontalSize;
    }

    public void ResetCamera()
    {



        
    }
}

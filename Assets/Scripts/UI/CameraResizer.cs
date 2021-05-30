using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Common;

public class CameraResizer : Singleton<CameraResizer>
{
    public int height;
    public int width;

    public int borderSize;

    public LeanConstrainToBox borderBox;

    public void ResizeCamera(int width = 1, int height = 1)
    {
        Debug.Log($"Camera Resizer Gelen Widht, Height: {width}, {height}");
        this.width = width;
        this.height = height;
        Camera.main.transform.position = new Vector3((float)(width - 1f) / 2f, (float)(height - 1f) / 2f, -10f);
        float aspectRatio = ((float)Screen.width) * 0.99f / ((float)Screen.height * 0.82f);
        float verticalSize = height / 2f + borderSize;
        float horizontalSize = (width / 2f + borderSize) / aspectRatio;

        if (verticalSize > horizontalSize)
        {
            Camera.main.orthographicSize = verticalSize * 1.7f;
            Debug.Log("Bölüm Görsel Olarak Dikey");
        }
        else
        {
            Camera.main.orthographicSize = horizontalSize * 1.5f;
            Debug.Log("Bölüm Görsel Olarak Yatay");
        }
        GetComponent<LeanPinchCamera>().Zoom = Camera.main.orthographicSize;
    }

    public void BorderCalculate(int borderX, int borderY)
    {
        borderBox.Size = new Vector3(borderX + 1, borderY + 1, -10);
        borderBox.Center = new Vector3((borderX + 1) / 2, (borderY + 1) / 2, -10);
    }

    public void ResizeCameraAgain()
    {
        ResizeCamera(width, height);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine("TakeScreenShot");
            
        }


    }

    public IEnumerator TakeScreenShot()
    {

        for (int i = 0; i < LevelManager.Instance.levels.levelsList.Count; i++)
        {
            ScreenCapture.CaptureScreenshot($"Screenshot_{GameManager.Instance.levelLoader.loadedLevel.levelID}.png");      
            yield return new WaitForSeconds(0.5f);
            LevelDetailsUIManager.Instance.LoadNextLevel();
            yield return new WaitForSeconds(0.5f);
        }
        
        



    }
}

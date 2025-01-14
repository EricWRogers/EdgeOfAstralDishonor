using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class NewBehaviourScript : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ScreenCapture.CaptureScreenshot("GameScreenshot-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".png");
            StartCoroutine(CoroutineScreenshot());
        }
    }

    private IEnumerator CoroutineScreenshot()
    {
        yield return new WaitForEndOfFrame();
        int width = Screen.width;
        int height = Screen.height;
        Texture2D screenshotTexture = new Texture2D(width, height, TextureFormat.ARGB32, false);
        Rect rect = new Rect(0, 0, width, height);
        screenshotTexture.ReadPixels(rect, 0, 0);
        screenshotTexture.Apply();

        byte[] byteArray = screenshotTexture.EncodeToPNG();
        System.IO.File.WriteAllBytes(Application.dataPath + "/CameraScreenshot.png", byteArray);
    }


}

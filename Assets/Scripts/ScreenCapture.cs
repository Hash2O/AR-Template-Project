using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScreenCapture : MonoBehaviour
{
    //Render texture to photo
    //https://docs.unity3d.com/ScriptReference/Camera.Render.html

    //Persistent Data Path
    //https://docs.unity3d.com/ScriptReference/Application-persistentDataPath.html

    public void MakeScreenShot()
    {
        StartCoroutine(ScreenShot());
    }


    IEnumerator ScreenShot()
    {
        yield return new WaitForEndOfFrame();

        Camera camera = Camera.main;
        int width = Screen.width;
        int height = Screen.height;
        RenderTexture rt = new RenderTexture(width, height, 24);
        camera.targetTexture = rt;

        // The Render Texture in RenderTexture.active is the one
        // that will be read by ReadPixels.
        var currentRT = RenderTexture.active;
        RenderTexture.active = rt;

        // Render the camera's view.
        camera.Render();

        // Make a new texture and read the active Render Texture into it.
        Texture2D image = new Texture2D(width, height);
        image.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        image.Apply();

        camera.targetTexture = null;

        // Replace the original active Render Texture.
        RenderTexture.active = currentRT;

        //Creating snapshot
        byte[] bytes = image.EncodeToPNG();
        string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
        string filePath = Path.Combine(Application.dataPath, fileName);     //For PC Testing, create a Screenshot folder inside project and add "+ "/Screenshots"" to Application.dataPath
        File.WriteAllBytes(filePath, bytes);

        //Cleaning
        Destroy(rt);
        Destroy(image);
    }
}

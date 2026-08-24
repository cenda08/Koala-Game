using UnityEngine;

public class ScreenShotScript : MonoBehaviour
{    void OnMouseDown()
    {
        ScreenCapture.CaptureScreenshot("KoalaGameScreenshot.png");
        Debug.Log("Screenshot has been taken!");
    }

}

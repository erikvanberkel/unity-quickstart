using System;
using UnityEngine;
using UnityEngine.UI;

public class WebCam : MonoBehaviour
{
    public RawImage rawImage;
    public Text debug;
    public int deviceWidth;
    public int deviceHeight;

    private DeviceOrientation lastOrientation;

    void Start()
    {
        this.deviceWidth = Screen.width > Screen.height ? Screen.width : Screen.height;
        this.deviceHeight = Screen.width > Screen.height ? Screen.height : Screen.width;
        GetComponent<RectTransform>().sizeDelta = new Vector2(this.deviceWidth, this.deviceHeight);
        UpdateWebCam();
    }

    private void UpdateWebCam()
    {
        this.lastOrientation = Input.deviceOrientation;
        foreach (var device in WebCamTexture.devices)
        {
            if (device.isFrontFacing)
            {
                continue;
            }
            var webCamTexture = new WebCamTexture(device.name);
            webCamTexture.Play();

            GetComponent<RectTransform>().sizeDelta = new Vector2(this.deviceWidth, this.deviceHeight);
            GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, OrientationToRotation(this.lastOrientation));
            this.rawImage.material.mainTexture = webCamTexture;
            this.debug.text = this.lastOrientation.ToString();
        }
    }

    void Update()
    {
        var transform = GetComponent<RectTransform>();
        if (this.lastOrientation != Input.deviceOrientation)
        {
            UpdateWebCam();
        }
   }

    private static float OrientationToRotation(DeviceOrientation orientation)
    {
        switch (orientation)
        {
            case DeviceOrientation.Unknown:
                return 0.0f;
            case DeviceOrientation.Portrait:
                return -90.0f;
            case DeviceOrientation.PortraitUpsideDown:
                return 90.0f;
            case DeviceOrientation.LandscapeLeft:
                return 0.0f;
            case DeviceOrientation.LandscapeRight:
                return 180.0f;
            case DeviceOrientation.FaceUp:
                return 0.0f;
            case DeviceOrientation.FaceDown:
                return 0.0f;
            default:
                return 0.0f;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebCamController : MonoBehaviour
{
    public Text logText;

    private bool camAvailable;

    private WebCamTexture webcamTexture;

    private Material material;

    private Vector4 uv_m;

    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            logText.text = "No camera detected";
            camAvailable = false;
        }

        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing)
            {
                logText.text = "Detect front camera";
                webcamTexture = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            }               
        }

        webcamTexture.Play();
        material = GetComponent<RawImage>().material;
        material.SetTexture("_MainTex", webcamTexture);

        uv_m = new Vector4(0f, 0f, 1f, 1f);
        material.SetVector("uv_m", uv_m);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void FlipHorizontal()
    {
        if (uv_m.x == 0f)
        {
            uv_m.x = 1f;
            uv_m.z = -1f;
        }
        else
        {
            uv_m.x = 0f;
            uv_m.z = 1f;
        }

        material.SetVector("uv_m", uv_m);
    }

    public void FlipVertical()
    {
        if (uv_m.y == 0f)
        {
            uv_m.y = 1f;
            uv_m.w = -1f;
        }
        else
        {
            uv_m.y = 0f;
            uv_m.w = 1f;
        }

        material.SetVector("uv_m", uv_m);
    }
}

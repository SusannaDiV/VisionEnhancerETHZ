using System.Collections;
using UnityEngine;

public class WebcamScript : MonoBehaviour
{
    WebCamTexture tex;

    [SerializeField]
    Material webcamMat;

    void Start()
    {
        if (WebCamTexture.devices.Length > 0)
        {
            WebCamDevice device = WebCamTexture.devices[0];
            tex = new WebCamTexture(device.name);
            webcamMat.SetTexture("_MainTex", tex);
            tex.Play();
        }
    }

    void Update()
    {
        if (tex != null && tex.isPlaying)
        {
            // try out with the first mode (deuteranopia)
            webcamMat.SetFloat("_Zoom", 0.3f);
            webcamMat.SetVector("_Offset", new Vector2(0.0f, 0.0f));
            webcamMat.SetInt("_Mode", 1); 
        }
    }
}

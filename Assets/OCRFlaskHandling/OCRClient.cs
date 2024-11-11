using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class OCRClient : MonoBehaviour
{
    private string flaskURL = "http://<YOUR_FLASK_SERVER_IP>:5000/ocr";

    public IEnumerator SendImageToOCR(byte[] imageData)
    {
        WWWForm form = new WWWForm();
        form.AddBinaryData("image", imageData, "photo.jpg", "image/jpeg");

        using (UnityWebRequest request = UnityWebRequest.Post(flaskURL, form))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Response: " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error: " + request.error);
            }
        }
    }

    public void CaptureAndSendImage(Texture2D texture)
    {
        byte[] imageData = texture.EncodeToJPG();
        StartCoroutine(SendImageToOCR(imageData));
    }
}

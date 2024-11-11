using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class OCRTextDisplay : MonoBehaviour
{
    [SerializeField] private string flaskURL = "http://141.153.234:5000/ocr";
    [SerializeField] private Text ocrTextDisplay;

    public IEnumerator SendImageToOCR(byte[] imageData)
    {
        WWWForm form = new WWWForm();
        form.AddBinaryData("image", imageData, "photo.jpg", "image/jpeg");

        using (UnityWebRequest request = UnityWebRequest.Post(flaskURL, form))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string responseText = request.downloadHandler.text;
                DisplayOCRResult(responseText);
            }
            else
            {
                Debug.LogError("Error: " + request.error);
                ocrTextDisplay.text = "Error fetching OCR data";
            }
        }
    }

    private void DisplayOCRResult(string responseText)
    {
        // Extract the text value from the JSON response
        string extractedText = ParseJSONResponse(responseText);
        ocrTextDisplay.text = extractedText;
    }


    private string ParseJSONResponse(string jsonResponse)
    {
        // Assuming response is in {"extracted_text": "some text"} format
        JSONObject jsonObject = new JSONObject(jsonResponse);
        return jsonObject.GetField("extracted_text").str;
    }

    public void CaptureAndSendImage(Texture2D texture)
    {
        byte[] imageData = texture.EncodeToJPG();
        StartCoroutine(SendImageToOCR(imageData));
    }
}

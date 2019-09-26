using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System.Collections;

using TMPro;

public class Signup : MonoBehaviour
{
    public TMP_InputField username;
    public TMP_InputField password;
    public TMP_Text errorTxt;
    public Login login;
    
    public void Execute()
    {
        errorTxt.SetText("Signing up...");
        StartCoroutine(AsynchExecute());
    }

    IEnumerator AsynchExecute() {
        
        byte[] bodyRaw = Encoding.UTF8.GetBytes("{\"user\": {\"username\": \""+ username.text + "\",\"password\": \"" + password.text + "\"}}");
        UnityWebRequest request = new UnityWebRequest("http://"+Env.playerApiHost+"/player", "POST");

        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        errorTxt.SetText("");
        if (request.isHttpError)
        {
            errorTxt.SetText(request.downloadHandler.text);
        }
        else if (request.isNetworkError)
        {
            errorTxt.SetText("Network error.");
        }
        else
        {
            login.Execute(username.text, password.text);
        }
    }
}

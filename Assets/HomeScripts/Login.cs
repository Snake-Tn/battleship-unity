using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System.Collections;

using TMPro;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public TMP_InputField username;
    public TMP_InputField password;
    public TMP_Text errorTxt;

    public void Execute(){
        errorTxt.SetText("");
        StartCoroutine(AsynchExecute());
    }

    public void Execute(string username, string password)
    {
        errorTxt.SetText("Signing in ...");
        this.username.text = username;
        this.password.text = password;
        StartCoroutine(AsynchExecute());
    }

    IEnumerator AsynchExecute(){
        
        byte[] bodyRaw = Encoding.UTF8.GetBytes("{\"username\": \"" + username.text + "\",\"password\": \"" + password.text + "\"}");
        UnityWebRequest request = new UnityWebRequest("http://" + Env.iamApiHost + "/authenticate", "POST");

        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        errorTxt.SetText("");
        Debug.Log(request.downloadHandler.text);
        if (request.isHttpError)
        {
            errorTxt.SetText(request.downloadHandler.text);
        }else if(request.isNetworkError){
            errorTxt.SetText("Network error.");
        }
        else {            
            TokenContainer.content = JsonUtility.FromJson<Token>(request.downloadHandler.text);
            TokenContainer.content.username = username.text;
            SceneManager.LoadScene("Lobby");
        }
        

        
    }
}

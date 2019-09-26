using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public Text ErrorTxt;

    public void Execute()
    {        
        ErrorTxt.text = "Starting game...";
        StartCoroutine(AsynchExecute());
    }



    IEnumerator AsynchExecute()
    {
        byte[] bodyRaw = Encoding.UTF8.GetBytes("{\"host\": \"" + TokenContainer.content.username + "\"}");
        UnityWebRequest request = new UnityWebRequest("http://" + Env.gameApiHost + "/games", "POST");

        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + TokenContainer.content.access_token);
        yield return request.SendWebRequest();

        if (request.isHttpError && request.responseCode > 500)
        {
            ErrorTxt.text = request.downloadHandler.text;
        }
        else if (request.isNetworkError)
        {
            ErrorTxt.text = "Network error.";
        }
        else if (request.responseCode != 200)
        {
            ErrorTxt.text = "Not Created. code=" + request.responseCode;
        }
        else
        {
           
            ErrorTxt.text = "";
            ResourceId resourceId = JsonUtility.FromJson<ResourceId>(request.downloadHandler.text);
            MyGame.Id = resourceId.id;
            Debug.Log(request.downloadHandler.text);
            SceneManager.LoadScene("GameBoard");

        }
    }

}
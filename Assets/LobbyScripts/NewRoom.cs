using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class NewRoom : MonoBehaviour
{
    public TMP_Text TitleTxt;
    public Text ErrorTxt;
    public RectTransform NewRoomPanel;
    public RectTransform MyGamePanel;
    public Text MyGame_Username;
    public Text MyGame_Title;    

    public void Execute()
    {
        ErrorTxt.text = "Creating room...";
        StartCoroutine(AsynchExecute());
    }

    IEnumerator AsynchExecute()
    {
        byte[] bodyRaw = Encoding.UTF8.GetBytes("{\"title\": \"" + TitleTxt.text + "\",\"host\": \"" + TokenContainer.content.username + "\"}");
        UnityWebRequest request = new UnityWebRequest("http://" + Env.lobbyApiHost + "/rooms", "POST");

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
            TitleTxt.text = "";
            ErrorTxt.text = "";
            ResourceId resourceId = JsonUtility.FromJson<ResourceId>(request.downloadHandler.text);
            Debug.Log(request.downloadHandler.text);
            MyRoom.id = resourceId.id;

            NewRoomPanel.gameObject.SetActive(false);
            MyGamePanel.gameObject.SetActive(true);

            MyGame_Username.text = TokenContainer.content.username;
            MyGame_Title.text = TitleTxt.text;

            RoomStatus.SetStatus(RoomStatus.HOSTED_ROOM);
        }
    }
}
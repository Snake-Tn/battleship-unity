using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class CloseRoom : MonoBehaviour
{
    public Text ErrorTxt;
    
    public RectTransform NewRoomPanel;
    public RectTransform MyGamePanel;

    public void Execute()
    {
        ErrorTxt.text = "Closing room...";
        StartCoroutine(AsynchExecute());
    }

    IEnumerator AsynchExecute()
    {
        string roomId = MyRoom.id;
        UnityWebRequest request = new UnityWebRequest("http://" + Env.lobbyApiHost + "/rooms/" + roomId, "DELETE");

        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

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
            ErrorTxt.text = "Not Closed. code=" + request.responseCode;
        }
        else
        {
            ErrorTxt.text = "";
            NewRoomPanel.gameObject.SetActive(true);
            MyGamePanel.gameObject.SetActive(false);
            RoomStatus.SetStatus(RoomStatus.FREE);
        }
    }
}

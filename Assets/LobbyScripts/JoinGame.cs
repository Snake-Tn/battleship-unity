using UnityEngine;
using UnityEngine.Networking;

using System.Collections;
using UnityEngine.UI;

public class JoinGame : MonoBehaviour
{
    public RectTransform NewGamePanel;
    public RectTransform JoinedGamePanel;

    public void Execute()
    {
        Debug.Log("joining game");
        StartCoroutine(AsynchExecute());
    }

    IEnumerator AsynchExecute()
    {
		int roomId = this.GetComponent<GameRaw>().Id;
        string roomHost = this.GetComponent<GameRaw>().HostTxt.text;

        UnityWebRequest request = new UnityWebRequest("http://" + Env.lobbyApiHost + "/rooms/" + roomId + "/guest/self", "POST");   
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + TokenContainer.content.access_token);
        yield return request.SendWebRequest();

        if (request.isHttpError && request.responseCode > 500)
        {
            Debug.Log(request.downloadHandler.text);
        }
        else if (request.isNetworkError)
        {
            Debug.Log("Network error.");
            
        }
        else if (request.responseCode != 201)
        {            
            Debug.Log("Did not join. code=" + request.responseCode);
        }
        else
        {
            GetComponent<GameRaw>().NewGamePanel.gameObject.SetActive(false);
            GetComponent<GameRaw>().JoinedGamePanel.gameObject.SetActive(true);

            GameObject.Find("JoinedGame_Username").GetComponent<Text>().text = TokenContainer.content.username;
            GameObject.Find("JoinedGame_Opponent").GetComponent<Text>().text = GetComponent<GameRaw>().HostTxt.text;
            GameObject.Find("JoinedGame_Desc").GetComponent<Text>().text = GetComponent<GameRaw>().TitleTxt.text;


            foreach (GameObject joinBtn in GameObject.FindGameObjectsWithTag("join-btn"))
			{
				joinBtn.GetComponent<Button>().interactable = false;
			}
            
			RoomStatus.SetStatus(RoomStatus.JOINED_ROOM);

			MyRoom.id = roomId.ToString();
            MyRoom.host = roomHost;

            Debug.Log("Joined!");
		}
    }
}

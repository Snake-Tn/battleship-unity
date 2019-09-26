using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TryToJoinGame : MonoBehaviour
{
    public Text ErrorTxt;
    public void Awake()
    {
        StartCoroutine(this.AsynchExecute());
    }

    IEnumerator AsynchExecute()
    {
        while (true)
        {
            Debug.Log("trying to join Game!" + MyRoom.host);
            
            UnityWebRequest request = new UnityWebRequest("http://" + Env.gameApiHost + "/games/" + MyRoom.host + "/guest/self", "POST");

            
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
				Debug.Log("Did not join. code=" + request.responseCode);
			}
            else
            {

                ErrorTxt.text = "";
                ResourceId resourceId = JsonUtility.FromJson<ResourceId>(request.downloadHandler.text);
                MyGame.Id = resourceId.id;
                Debug.Log(request.downloadHandler.text);
                SceneManager.LoadScene("GameBoard");

            }


            yield return new WaitForSeconds(3f);
        }
    }

}
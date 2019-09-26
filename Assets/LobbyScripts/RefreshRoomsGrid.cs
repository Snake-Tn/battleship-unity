using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class RefreshRoomsGrid:MonoBehaviour
{
    public GameObject GameRawPrefab;
    private List<GameObject> gameRaws = new List<GameObject>();
    public RectTransform NewGamePanel;
    public RectTransform JoinedGamePanel;

    public Text MyGame_Opponent;

    public Button StartGame;

    public void Awake()
    {        
        StartCoroutine(this.AsynchExecute());
    }
    
    IEnumerator AsynchExecute()
    {
        while (true)
        {
            UnityWebRequest request = UnityWebRequest.Get("http://" + Env.lobbyApiHost + "/rooms");
            request.SetRequestHeader("Authorization", "Bearer " + TokenContainer.content.access_token);
           
            yield return request.SendWebRequest();

            Room[] rooms = JsonHelper.getJsonArray<Room>(request.downloadHandler.text);

            float height = GameRawPrefab.GetComponent<RectTransform>().rect.height + 5;
            int y = 0;

            gameRaws.ForEach(gameRaw =>
            {
                Destroy(gameRaw);
            });


            foreach (Room room in rooms)
            {
                if (room.guest.Equals(TokenContainer.content.username)) {
                    continue;
                }
                if(room.host.Equals(TokenContainer.content.username)) {                   
                    if (room.guest.Length > 0)
                    {
                        MyGame_Opponent.text = room.guest;
                        StartGame.interactable = true;
                    }
                    else {
                        MyGame_Opponent.text = "??????";
                        StartGame.interactable = false;
                    }
                    continue;
                }

                GameObject gameRaw = Instantiate(GameRawPrefab, new Vector3(0, -1 * height * y - 65, 0), Quaternion.identity);

                gameRaws.Add(gameRaw);

                gameRaw.GetComponent<GameRaw>().SetHost(room.host);
               
                if (room.guest.Length > 0)
                {
                    gameRaw.GetComponent<GameRaw>().SetGuest(room.guest);
                }

                gameRaw.GetComponent<GameRaw>().SetTitle(room.title);
                gameRaw.GetComponent<GameRaw>().SetId(room.id);

                gameRaw.GetComponent<GameRaw>().SetNewGoomPanel(NewGamePanel);
                gameRaw.GetComponent<GameRaw>().SetJoinedGamePanel(JoinedGamePanel);

                y++;
                gameRaw.transform.SetParent(transform, false);
                if (y % 2 == 0)
                {
                    gameRaw.GetComponent<Image>().color = new Color(0.54f, 0.54f, 0.54f, 0.2f);
                }

                GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height * y);
            }
            if (!RoomStatus.Status.Equals(RoomStatus.FREE)) {
                foreach (GameObject joinBtn in GameObject.FindGameObjectsWithTag("join-btn"))
                {
                    joinBtn.GetComponent<Button>().interactable = false;
                }
            }

            yield return new WaitForSeconds(3f);
        }
    }
}

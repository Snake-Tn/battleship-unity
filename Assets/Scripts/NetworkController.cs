using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkController : MonoBehaviour
{

    public TextAsset testMessage;
    public GameController GameController;


    public void Awake()
    {
        Connect();
    }

    public void Connect() {
        Debug.Log("connect");
    }

    public void Send(string msg) {

        Debug.Log(msg);
    }

    public void OnMessage(string msg) {
        Debug.Log(msg);
        IncomeMsgDto incomeMsgDto = JsonUtility.FromJson<IncomeMsgDto>(msg);
        if (incomeMsgDto.type.Equals("your-turn")) {
            GameController.YourTurn();
        }
        if (incomeMsgDto.type.Equals("opponent-finish-board-init"))
        {
            GameController.OpponentFinishedBoardInit();
        }

        if (incomeMsgDto.type.Equals("player-shoot-impact"))
        {
            GameController.PlayerShootImpact();
        }
    } 


    public void DebugOnMessage() {
        OnMessage(testMessage.text);
    }
}

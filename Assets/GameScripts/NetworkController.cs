using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkController : MonoBehaviour
{

    public TextAsset testMessage;
    public GameController GameController;
    public WebSocket WebSocket;


    public void Awake()
    {
        Connect();
    }

    public void Connect() {
        WebSocket.Connect();
    }

    public void Send(string msg) {

        WebSocket.Send(msg);
    }

    public void OnMessage(string msg) {
        Debug.Log(msg);
        IncomeMsgDto incomeMsgDto = JsonUtility.FromJson<IncomeMsgDto>(msg);

        if (incomeMsgDto.type.Equals("opponent-connected"))
        {
            GameController.OpponentConnected();
        }

        if (incomeMsgDto.type.Equals("opponent-finish-board-init"))
        {
            GameController.OpponentFinishedBoardInit();
        }

        if (incomeMsgDto.type.Equals("player-shoot-impact"))
        {
            ShootImpactDto shootImpactDto = JsonUtility.FromJson<ShootImpactDto>(msg);            
            GameController.PlayerShootImpact(shootImpactDto);
        }

        if (incomeMsgDto.type.Equals("opponent-shoot-impact"))
        {
            ShootImpactDto shootImpactDto = JsonUtility.FromJson<ShootImpactDto>(msg);
            GameController.OpponentShootImpact(shootImpactDto);
        }

        if (incomeMsgDto.type.Equals("you-lost"))
        {
         
            GameController.YouLost();
        }
        if (incomeMsgDto.type.Equals("you-won"))
        {
          
            GameController.YouWon();
        }
    } 


    public void DebugOnMessage() {
        OnMessage(testMessage.text);
    }
}

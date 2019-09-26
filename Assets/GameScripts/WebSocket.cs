
using UnityEngine;
using System.Runtime.InteropServices;

public class WebSocket : MonoBehaviour
{
    public NetworkController NetworkController;

    [DllImport("__Internal")]
    private static extern void JSConnect(string token, string gameid);

    [DllImport("__Internal")]
    private static extern void JSSend(string msg);

    public void Connect()
    {
        Debug.Log("CONNECT!");
        JSConnect(TokenContainer.content.access_token, MyGame.Id);
    }

    public void Send(string msg) {
        JSSend(msg);
    }

    public void OnMessage(string message) {
        Debug.Log(message);
        NetworkController.OnMessage(message);
    }
}

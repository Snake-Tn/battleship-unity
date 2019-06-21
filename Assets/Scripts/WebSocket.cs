using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class WebSocket : MonoBehaviour
{

    public NetworkController NetworkController;

    [DllImport("__Internal")]
    private static extern void JSConnect();

    [DllImport("__Internal")]
    private static extern void JSSend(string msg);

    public void Connect()
    {
        Debug.Log("CONNECT!");
        JSConnect();
    }

    public void Send(string msg) {
        JSSend(msg);
    }

    public void OnMessage(string message) {
        Debug.Log(message);
        NetworkController.OnMessage(message);
    }
}

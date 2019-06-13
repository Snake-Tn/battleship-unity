using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class WebSocket : MonoBehaviour
{
    public void Send() { 
    }

    public void Connect() {
        Debug.Log("CONNECT!");
    }

    [DllImport("__Internal")]
    private static extern void Hello();

    public void ExecHello() {
        Hello();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

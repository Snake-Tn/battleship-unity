using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private bool isHit = false;
    private bool isMiss = false;

    public Coordination Coordination;

    public void SetCoordinate(Coordination c) {
        Coordination = c;
    }

    public void SetIsHit() {
        GetComponent<Image>().color = Color.red;
    }

    public void SetIsMiss() {
        GetComponent<Image>().color = Color.blue;
    }
}

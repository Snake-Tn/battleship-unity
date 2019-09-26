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
        isHit = true;
        GetComponent<Image>().color = Color.red;
    }

    public void SetIsMiss() {
        isMiss = true;
        GetComponent<Image>().color = Color.blue;
    }

    public bool IsHit() {
        return isHit;
    }
    public bool IsMiss()
    {
        return isMiss;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int Size;

    public int Orientation = 1;

    public Coordination Coordination = new Coordination(0,0);

    public void Rotate() {
        transform.Rotate(Vector3.forward, 90f);
        Orientation++;
        if (Orientation > 4) {
            Orientation = 1;
        }
    }

}

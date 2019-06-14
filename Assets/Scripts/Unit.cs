using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public void UpdateColor()
    {
        if (!HasAcceptablePosition()) {
            GetComponent<Image>().color = Color.red;
            Debug.Log(GetComponent<Image>().color);

        }
        else {
            GetComponent<Image>().color = Color.white;
        }
    }
    public int Size;

    public int Orientation = 1;

    private Coordination getCoordination() {

        float xCenter = transform.localPosition.x + 150;
        float yCenter = transform.localPosition.y + 150;
        float distanceToBackCenter = 50 * (((float)Size/2) - 0.5f);
               
        if (Orientation == 1)
        {
            yCenter -= distanceToBackCenter;
        }
        if (Orientation == 2)
        {
            xCenter+= distanceToBackCenter;
        }
        if (Orientation == 3)
        {
            yCenter += distanceToBackCenter;
        }
        if (Orientation == 4)
        {
            xCenter -= distanceToBackCenter;
        }
        return new Coordination((int)xCenter / 50 + 1, (int)yCenter / 50 + 1);
    }

    public void Rotate() {
    
        transform.Rotate(Vector3.forward, 90f);
        if (Size % 2 == 0)
        {
            float xDirection = 1;
            float yDirection = 1;

            if (Orientation == 1)
            {
                xDirection = -1;
                yDirection = -1;
            }
            if (Orientation==2) {
                yDirection = -1;
            }
            if (Orientation == 4)
            {
                xDirection = -1;
            }
         
            transform.localPosition = new Vector3(transform.localPosition.x + 25* xDirection, transform.localPosition.y + 25* yDirection, 0);
        }
        Orientation++;
        if (Orientation > 4) {
            Orientation = 1;
        }
        UpdateColor();
    }



    public bool HasAcceptablePosition()
    {
        Coordination coordination = getCoordination();
        Debug.Log(coordination.x);
        if(coordination.x<=0 || coordination.y<=0 || coordination.x>7 || coordination.y > 7) {
            return false;
        }
        if (Orientation == 1 && coordination.y - 1 + Size<= 7) {
            return true;
        }
        if (Orientation == 2 && coordination.x - Size >= 0)
        {
            return true;
        }
        if (Orientation == 3 && coordination.y - Size >= 0)
        {
            return true;
        }
        if (Orientation == 4 && coordination.x - 1 + Size <= 7)
        {
            return true;
        }
        return false;

    }

}

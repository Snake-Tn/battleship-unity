using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public void UpdateColor()
    {
        if (!HasAcceptablePosition()) {
            GetComponent<Image>().color = new Color(0.8f, 0f, 0f, 0.6f);
        }
        else {
            GetComponent<Image>().color = new Color(0.33f, 0.33f, 0.49f,0.5f);
        }
        GameObject.Find("start-btn").GetComponent<StartButton>().UpdateStatus();
    }

    public int Size;

    public int Orientation = 1;

    public Coordination GetCoordination() {

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
        if (isCollidingWithOtherUnit()) {
            return false;
        }
        Coordination coordination = GetCoordination();
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

    private bool isCollidingWithOtherUnit() {
        GameObject[] units;
        units = GameObject.FindGameObjectsWithTag("player-unit");
        foreach(GameObject unit in units) {
            if (unit.Equals(gameObject)) {
                continue;
            }

            Coordination oCoordination = unit.GetComponent<Unit>().GetCoordination();
            Coordination cCoordination = GetCoordination();
            int oSize = unit.GetComponent<Unit>().Size;
            int oOrientation = unit.GetComponent<Unit>().Orientation;
            int cx = cCoordination.x;
            int cy = cCoordination.y;
            int ox ;
            int oy ;
            for (int cIndex = 0; cIndex < Size; cIndex++) {
                 ox = oCoordination.x;
                 oy = oCoordination.y;
                for (int oIndex = 0; oIndex < oSize; oIndex++)
                {                   
                    if (cx == ox && cy == oy) {
                        return true;
                    }
                    if (oOrientation == 1)
                    {
                        oy++;
                    }
                    if (oOrientation == 2)
                    {
                        ox--;
                    }
                    if (oOrientation == 3)
                    {
                        oy--;
                    }
                    if (oOrientation == 4)
                    {
                        ox++;
                    }
                }

                if (Orientation == 1)
                {
                    cy++;
                }
                if (Orientation == 2)
                {
                    cx--;
                }
                if (Orientation == 3)
                {
                    cy--;
                }
                if (Orientation == 4)
                {
                    cx++;
                }
            }

        }
        return false;
    }

}

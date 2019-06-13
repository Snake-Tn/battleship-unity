using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    public GameObject slotPrefab;
    public GameObject playerBoard;
    public GameObject opponentBoard;
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        float width = slotPrefab.GetComponent<RectTransform>().rect.width;

        GenerateBoard(playerBoard.transform, width);
        GenerateBoard(opponentBoard.transform, width);
    }

    private void GenerateUnitsSelection() { 
        
    }

    private void GenerateBoard(Transform originTransform, float width)
    {
        for (int x = -3; x < 4; x++)
        {
            for (int y = -3; y < 4; y++)
            {
                GameObject slot = Instantiate(slotPrefab, new Vector3(width * x, width * y, 0), Quaternion.identity);
                slot.GetComponent<Slot>().SetCoordinate(x,y);
                slot.transform.SetParent(originTransform, false);
            }
        }
    }

}

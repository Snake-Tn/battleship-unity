using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    public GameObject playerSlotPrefab;
    public GameObject opponentSlotPrefab;

    public GameObject playerBoard;
    public GameObject opponentBoard;
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        GenerateBoard(playerBoard.transform, playerSlotPrefab);
        GenerateBoard(opponentBoard.transform, opponentSlotPrefab);
    }

    private void GenerateUnitsSelection() { 
        
    }

    private void GenerateBoard(Transform originTransform, GameObject slotPrefab)
    {
        float width = slotPrefab.GetComponent<RectTransform>().rect.width;
        for (int x = -3; x < 4; x++)
        {
            for (int y = -3; y < 4; y++)
            {
                GameObject slot = Instantiate(slotPrefab, new Vector3(width * x, width * y, 0), Quaternion.identity);
                slot.GetComponent<Slot>().SetCoordinate(new Coordination( x+4,y+4));
                slot.transform.SetParent(originTransform, false);
            }
        }
    }

}

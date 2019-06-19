using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public NetworkController NetworkController;

    bool freeToFire = false;
    bool boardInitCompleted = false;

    public void InitBoard() {
        boardInitCompleted = true;
        InitBoardDto initBoardDto = new InitBoardDto();       

        GameObject[] units;
        units = GameObject.FindGameObjectsWithTag("player-unit");
       
        foreach (GameObject unit in units)
        {
            Unit unitComponent = unit.GetComponent<Unit>();
            initBoardDto.instruction.Add(new UnitDto(
                mapOrientation(unitComponent.Orientation),
                unitComponent.GetCoordination(),
                unitComponent.Size
                ));

        }        
        NetworkController.Send(JsonUtility.ToJson(initBoardDto));
        if (!freeToFire)
        {
            GameObject.Find("ActionDisplay").GetComponent<Animator>().SetTrigger("wait-for-opponent");
        }
        else {
            YourTurn();
        }
    }

    public void ExecutShoot(Coordination coordination) {
        freeToFire = false;
        GameObject.Find("fire-btn").GetComponent<Button>().interactable = false;
        ExecShootDto execShootDto = new ExecShootDto(coordination);
        NetworkController.Send(JsonUtility.ToJson(execShootDto));
    }

    public void OpponentFinishedBoardInit() {
        freeToFire = true;
        GameObject.Find("ActionDisplay").GetComponent<Animator>().SetTrigger("free-to-fire");
        if (boardInitCompleted) {
            YourTurn();
        }
    }

    public void YourTurn() {
        GameObject.Find("FireBoard").GetComponent<Animator>().SetTrigger("free-to-fire");
    }

    public void OpponentShootImpact(string msg) {
        
    }
    public void PlayerShootImpact(string msg)
    {
        GameObject.Find("fire-btn").GetComponent<Button>().interactable = true;
    }

    private string mapOrientation(int orientation) {
        switch (orientation) {
            case 1:
                return "n";
            case 2:
                return "w";
            case 3:
                return "s";
            case 4:
                return "e";
        }
        throw new System.Exception();
    }
}

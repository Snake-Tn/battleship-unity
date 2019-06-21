using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public NetworkController NetworkController;
    public GameObject DestroyedUnitPrefab;
    public Transform PlayerBoard;
    public Transform OpponentBoard;

    bool freeToFire = false;
    bool boardInitCompleted = false;

    int executedShoots = 0;
    int receivedShootsCount = 0;
    Stack<ShootImpactDto> receivedShoots = new Stack<ShootImpactDto>();

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

        GameObject.Find("fire-btn").GetComponent<Button>().interactable = false;
        ExecShootDto execShootDto = new ExecShootDto(coordination);
        NetworkController.Send(JsonUtility.ToJson(execShootDto));
        executedShoots++;
        if (executedShoots == 3)
        {
            freeToFire = false;
            executedShoots = 0;
            GameObject.Find("FireBoard").GetComponent<Animator>().SetTrigger("shoot-executed");
        }
    }

    public void AfterExecutShoot() {
        Debug.Log("AfterExecutShoot");
        foreach (ShootImpactDto shootImpact in receivedShoots.ToArray()) {
            DrawShootImpact(shootImpact, "player-slot");
        }
        receivedShoots.Clear();
        if (receivedShootsCount == 3 && !freeToFire)
        {
            freeToFire = true;
            receivedShootsCount = 0;
            YourTurn();
        }
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

    private void DrawShootImpact(ShootImpactDto shootImpactDto, string slotsTag) {
        GameObject[] slots = GameObject.FindGameObjectsWithTag(slotsTag);
        foreach (GameObject slot in slots)
        {
            foreach (Coordination hit in shootImpactDto.hits)
            {
                Coordination c = slot.GetComponent<Slot>().Coordination;
                if (c.x == hit.x && c.y == hit.y)
                {
                    slot.GetComponent<Slot>().SetIsHit();
                }
            }
        }

        foreach (GameObject slot in slots)
        {
            foreach (Coordination hit in shootImpactDto.misses)
            {
                Coordination c = slot.GetComponent<Slot>().Coordination;
                if (c.x == hit.x && c.y == hit.y)
                {
                    slot.GetComponent<Slot>().SetIsMiss();
                }
            }
        }
    }

    public void OpponentShootImpact(ShootImpactDto shootImpactDto) {
        if (freeToFire)
        {
            receivedShoots.Push(shootImpactDto);            
        }
        else {
            DrawShootImpact(shootImpactDto, "player-slot");
        }
        
        receivedShootsCount++;
        if (receivedShootsCount ==3 && !freeToFire) {
            freeToFire = true;
            receivedShootsCount = 0;
            YourTurn();
        }
        DrawDestroyedUnits(shootImpactDto.destroyed_units, PlayerBoard);
    }

    public void PlayerShootImpact(ShootImpactDto shootImpactDto)
    {
        GameObject.Find("fire-btn").GetComponent<Button>().interactable = true;
        DrawShootImpact(shootImpactDto, "opponent-slot");
        DrawDestroyedUnits(shootImpactDto.destroyed_units, OpponentBoard);
    }

    public void DrawDestroyedUnits(UnitDto[] units, Transform board) {
        float width = DestroyedUnitPrefab.GetComponent<RectTransform>().rect.width;
        foreach (UnitDto unitDto in units) {
            float distanceToBackCenter = width * (((float)unitDto.size / 2) - 0.5f);
            Debug.Log(distanceToBackCenter);

            float xCenter = (unitDto.position.x - 4) * width;
            float yCenter = (unitDto.position.y - 4) * width;
            Debug.Log(yCenter);
            float rotation = 0;
            if (unitDto.orientation == "n")
            {
                yCenter += distanceToBackCenter;
            }
            if (unitDto.orientation == "w")
            {
                rotation = 90;
                xCenter -= distanceToBackCenter;
            }
            if (unitDto.orientation == "s")
            {
                rotation = 180;
                yCenter -= distanceToBackCenter;
            }
            if (unitDto.orientation == "e")
            {
                rotation = 270;
                xCenter += distanceToBackCenter;
            }
            
            GameObject unitGameObject = Instantiate(DestroyedUnitPrefab, Vector3.zero, Quaternion.identity);
            unitGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(width, width * unitDto.size);
            unitGameObject.GetComponent<RectTransform>().localPosition = new Vector3(xCenter, yCenter, 0);
            unitGameObject.GetComponent<RectTransform>().Rotate(Vector3.forward, rotation);
            unitGameObject.transform.SetParent(board,false);
        }
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

    public void YouLost() {
        GameObject.Find("Gameover").GetComponent<Animator>().SetTrigger("gameover");

    }
    public void YouWon() {
        GameObject.Find("Gameover").GetComponent<Animator>().SetTrigger("gameover");
    }
}

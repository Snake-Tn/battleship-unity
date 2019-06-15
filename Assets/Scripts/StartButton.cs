
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{

    public void UpdateStatus()
    {
        GameObject[] units;
        units = GameObject.FindGameObjectsWithTag("player-unit");
        bool active = true;
        foreach (GameObject unit in units)
        {
            if (!unit.GetComponent<Unit>().HasAcceptablePosition())
            {
                active = false;
            }
        }
        GetComponent<Button>().interactable = active;
    }

    public void OnClick() {
        GameObject[] units;
        units = GameObject.FindGameObjectsWithTag("player-unit");
       
        foreach (GameObject unit in units)
        {
            unit.transform.SetParent(GameObject.Find("PlayerBoard").transform);
            unit.GetComponent<UnitPositionner>().IsFrozen = true;
        }


        GameObject[] rotators;
        rotators = GameObject.FindGameObjectsWithTag("unit-rotator-btn");

        foreach (GameObject rotator in rotators)
        {
            rotator.SetActive(false);

        }

        GameObject.Find("UnitSelection").GetComponent<Animator>().SetTrigger("selection-complete");        
    }
}

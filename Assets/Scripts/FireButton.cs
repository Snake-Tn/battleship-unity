using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireButton : MonoBehaviour
{
    public GameController GameController;
    public void UpdateStatus() {

    }

    public void ExecuteShoot()
    {

        GameObject[] slots = GameObject.FindGameObjectsWithTag("opponent-slot");
        foreach (GameObject slot in slots)
        {
            if (slot.GetComponent<FireSlotSelector>().IsHit || !slot.GetComponent<FireSlotSelector>().IsSelected) {
                continue;
            }

            slot.GetComponent<FireSlotSelector>().IsHit = true;

            GameController.ExecutShoot(slot.GetComponent<Slot>().Coordination);
            break;
        }
    }
}

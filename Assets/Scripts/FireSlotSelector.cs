using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FireSlotSelector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Coordination firePosition;

    public void OnPointerClick(PointerEventData eventData)
    {
        firePosition = GetComponent<Slot>().Coordination;

        Debug.Log(firePosition.x);
        Debug.Log(firePosition.y);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        GetComponent<Image>().color = Color.red ;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = new Color(0.1174351f, 0.483185f, 0.8584906f);
    }


}

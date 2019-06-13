using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotPopulator : MonoBehaviour,IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        int unitSize = eventData.pointerDrag.GetComponent<Unit>().Size;
        int orientation = eventData.pointerDrag.GetComponent<Unit>().Orientation;

        float xTranslate = 0;
        float yTranslate = 0;
        float width = eventData.pointerDrag.GetComponent<RectTransform>().rect.width;

        if ( unitSize % 2 == 0) {
            if((orientation == 1 || orientation == 3)) {
                yTranslate = width / 2;
            }
            else {
                xTranslate = width / 2;
            }

        }       
        eventData.pointerDrag.transform.localPosition = new Vector3(transform.localPosition.x + xTranslate, transform.localPosition.y + yTranslate, 0f) ;
        eventData.pointerDrag.gameObject.SendMessage("SetIsPositionned",true);


    }
}

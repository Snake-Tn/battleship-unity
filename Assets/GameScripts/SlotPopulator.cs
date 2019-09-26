using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotPopulator : MonoBehaviour,IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {        
        if (eventData.pointerDrag.GetComponent<UnitPositionner>().IsFrozen) {
            return;
        }
        Vector3 mousePosition = transform.InverseTransformPoint(Input.mousePosition);

        int unitSize = eventData.pointerDrag.GetComponent<Unit>().Size;
        int orientation = eventData.pointerDrag.GetComponent<Unit>().Orientation;

        float xTranslate = 0;
        float yTranslate = 0;
        float width = eventData.pointerDrag.GetComponent<RectTransform>().rect.width;

        if ( unitSize % 2 == 0) {
            if((orientation == 1 || orientation == 3)) {
                yTranslate = mousePosition.y>=0 ? width / 2 : -1 * width / 2;
            }
            else {
                xTranslate = mousePosition.x >= 0 ? width / 2 : -1 * width / 2;
            }

        }       
        eventData.pointerDrag.transform.localPosition = new Vector3(transform.localPosition.x + xTranslate, transform.localPosition.y + yTranslate, 0f) ;
        eventData.pointerDrag.gameObject.SendMessage("SetIsPositionned",true);
        eventData.pointerDrag.gameObject.SendMessage("UpdateColor");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitPositionner : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    private Vector3 initialPosition;

    private bool isPositioned=true;

    public void OnDrag(PointerEventData eventData)
    {
        isPositioned = false;
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (!isPositioned) {
            transform.localPosition = initialPosition;
        }
    }

    public void SetIsPositionned() {
        isPositioned = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }


    public void Start()
    {
        initialPosition = transform.localPosition;
    }
}

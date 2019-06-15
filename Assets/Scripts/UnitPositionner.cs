using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitPositionner : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private bool isPositioned=true;
    public bool IsFrozen = false;

    public void OnDrag(PointerEventData eventData)
    {
        if (IsFrozen) {
            return;
        }
        isPositioned = false;
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (IsFrozen)
        {
            return;
        }
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (!isPositioned) {
            transform.localPosition = initialPosition;
            transform.localRotation = initialRotation;
            GetComponent<Unit>().Orientation = 1;
        }
    }

    public void SetIsPositionned() {
        isPositioned = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (IsFrozen)
        {
            return;
        }
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }


    public void Start()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
    }
}

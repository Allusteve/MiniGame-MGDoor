using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveWire : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        setNotThing(eventData);
    }
    public void OnDrag(PointerEventData eventData)
    {
        //拖拽移动图片
        SetDraggedPosition(eventData);
    }

    private void SetDraggedPosition(PointerEventData eventData)
    {
        var rt = gameObject.GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            rt.position = globalMousePos;
        }
    }


    private void setNotThing(PointerEventData eventData)
    {
        var rt = gameObject.GetComponent<RectTransform>();
        SetWire.IsNotSet(rt.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        setThing(eventData);
    }

    private void setThing(PointerEventData eventData)
    {
        var rt = gameObject.GetComponent<RectTransform>();
        rt.position = SetWire.NearestCood(rt.position);
        SetWire.IsSet(rt.position);
    }
}

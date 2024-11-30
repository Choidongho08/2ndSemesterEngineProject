using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnMouseEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public event Action onMouseEnter; 
    public event Action onMouseExit;

    public void OnPointerEnter(PointerEventData eventData)
    {
        onMouseEnter?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onMouseExit?.Invoke();
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseEnterExitEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public event Action OnEnterMouse;
    public event Action OnExitMouse;
    public Vector3 objectLocalScale;

    private void Start()
    {
        objectLocalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnEnterMouse?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnExitMouse?.Invoke();
    }
}

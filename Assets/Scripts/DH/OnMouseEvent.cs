using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseEvent : MonoBehaviour
{
    public event Action onMouseEnter; 
    public event Action onMouseExit;

    private void OnMouseEnter()
    {
        onMouseEnter?.Invoke();
    }
    private void OnMouseExit()
    {
        onMouseExit?.Invoke();
    }
}

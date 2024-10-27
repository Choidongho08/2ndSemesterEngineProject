using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class Loading : MonoSingleton<Loading>
{
    [SerializeField] private GameObject _child;

   
    public void Show()
    {
        _child.SetActive(true);
    }
    public void Hide()
    {
        _child.SetActive(false);
    }
}

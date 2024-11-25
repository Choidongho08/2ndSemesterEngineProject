using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SJ_ChangeBackground : MonoBehaviour
{
    [SerializeField] private List<GameObject> _backgroundList;
    [SerializeField] private GameObject _lastBackGround;

    private void Start()
    {
        _lastBackGround = _backgroundList[2];
        _lastBackGround.SetActive(true);
    }


    public void ChangeBackGroundBtn(int number)
    {
        _lastBackGround.SetActive(false);
        _backgroundList[number].SetActive(true);
        _lastBackGround = _backgroundList[number];
    }
    
    public void ChkChangeBackGroundBtn(int number)
    {
        if (InterGimick.ItemIsHere)
        {
            _lastBackGround.SetActive(false);
            _backgroundList[number].SetActive(true);
            _lastBackGround = _backgroundList[number];
        }
    }


}

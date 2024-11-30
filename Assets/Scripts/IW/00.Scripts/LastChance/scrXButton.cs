using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrXButton : MonoBehaviour
{
    [SerializeField] private Ease easyType = Ease.OutBack;
    [SerializeField] private GameObject _lastP; // Last Panel 객체

    private RectTransform _lastRect; // LastPanel의 RectTransform
    private Vector2 _originPosi; // LastPanel의 원래 위치

    private void Awake()
    {
        // _lastP 객체의 RectTransform을 가져옵니다.
        _lastRect = _lastP.GetComponent<RectTransform>();
        // _lastP의 원래 위치를 초기화
        _originPosi = _lastRect.anchoredPosition;
    }

    public void XButton()
    {
        // LastPanel을 원래 위치로 애니메이션을 실행하여 이동
        _lastRect.DOAnchorPos(_originPosi, 1f).SetEase(easyType);
    }
}

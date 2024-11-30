using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrXButton : MonoBehaviour
{
    [SerializeField] private Ease easyType = Ease.OutBack;
    [SerializeField] private GameObject _lastP; // Last Panel ��ü

    private RectTransform _lastRect; // LastPanel�� RectTransform
    private Vector2 _originPosi; // LastPanel�� ���� ��ġ

    private void Awake()
    {
        // _lastP ��ü�� RectTransform�� �����ɴϴ�.
        _lastRect = _lastP.GetComponent<RectTransform>();
        // _lastP�� ���� ��ġ�� �ʱ�ȭ
        _originPosi = _lastRect.anchoredPosition;
    }

    public void XButton()
    {
        // LastPanel�� ���� ��ġ�� �ִϸ��̼��� �����Ͽ� �̵�
        _lastRect.DOAnchorPos(_originPosi, 1f).SetEase(easyType);
    }
}

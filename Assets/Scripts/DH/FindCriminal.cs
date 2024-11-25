using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindCriminal : MonoBehaviour
{
    [SerializeField] private Button _goFindButton, _windowButton;
    [SerializeField] private Image _findFill;

    private Vector3 _windowPosition;
    private bool _isOpenWindow = false;
    private bool _isClick = false;
    private bool _isVote = false;
    private float _maxClickTime = 3f;
    private float _currentClickTime = 0f;
    private float _fill;
    
    public static event Action onVote;

    private void Awake()
    {
        _windowButton.onClick.AddListener(WindowOpening);
        _windowPosition = gameObject.transform.position;
    }
    private void Update()
    {
        if (_isClick && !_isVote)
        {
            _currentClickTime += Time.deltaTime;
            _fill = _currentClickTime / _maxClickTime;
            _findFill.fillAmount = _fill;
            if (_currentClickTime > _maxClickTime)
            {
                Debug.Log("Vote");
                onVote?.Invoke();
                _isVote = true;
                PointerUp();
                return;
            }
        }
    }

    public void PointerDown()
    {
        _isClick = true;
    }
    public void PointerUp()
    {
        _isClick = false;
        _fill = 0;
        _findFill.fillAmount = _fill;
        _currentClickTime = 0;
    }
    private void WindowOpening()
    {
        if(!_isOpenWindow)
        {
            _isOpenWindow = true; // ø≠æÓ¡‹

            gameObject.transform.DOKill();
            gameObject.transform.DOMoveX(2160f, 0.3f);
            _windowButton.GetComponentInChildren<Image>().transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            _isOpenWindow = false; // ¥›æ∆¡‹

            gameObject.transform.DOKill();
            gameObject.transform.DOMoveX(2830f, 0.3f);
            _windowButton.GetComponentInChildren<Image>().transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}

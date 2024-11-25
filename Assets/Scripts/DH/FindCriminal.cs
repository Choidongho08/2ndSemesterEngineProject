using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
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
    private float _voteFill;
    private int _voteCount;

    public static FindCriminal Singleton;
    public event Action OnFindCriminal;

    private void Awake()
    {
        FindCriminal.Singleton = this;
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
                _isVote = true;
                PointerUp();
                VoteServerRpc();
                return;
            }
        }
    }
    public void PointerDown()
    {
        _findFill.fillAmount = 0f;
        _isClick = true;
    }
    public void PointerUp()
    {
        _isClick = false;
        _currentClickTime = 0;
    }
    private void WindowOpening()
    {
        if(!_isOpenWindow)
        {
            _isOpenWindow = true; // 열어줌

            gameObject.transform.DOKill();
            gameObject.transform.DOMoveX(2160f, 0.3f);
            _windowButton.GetComponentInChildren<Image>().transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            _isOpenWindow = false; // 닫아줌

            gameObject.transform.DOKill();
            gameObject.transform.DOMoveX(2830f, 0.3f);
            _windowButton.GetComponentInChildren<Image>().transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void Vote()
    {
        _voteCount++;
        _voteFill += 0.5000f;
        _findFill.fillAmount = _voteFill;
        if ( _voteCount == 2)
        {
            OnFindCriminal?.Invoke();
            Debug.Log("GoCriminal");
        }
    }
    [ServerRpc]
    private void VoteServerRpc() // 클라가 서버한테
    {
        VoteClientRpc();
    }
    [ClientRpc]
    private void VoteClientRpc() // 서버가 클라들한테
    {
        FindCriminal.Singleton.Vote();
    }
    
}

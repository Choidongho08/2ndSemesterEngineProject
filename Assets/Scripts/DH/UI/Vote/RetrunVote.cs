using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReturnVote : NetworkBehaviour
{
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _voteButton;
    [SerializeField] private Image _openImage;
    [SerializeField] private float _duration;
    [SerializeField] private float _voteMaxTime;

    private float _voteCurrentTime = 0f;
    private float _currentFillAmount = 0f;
    private bool _isOpen = false;
    private bool _isVote = false;
    private bool _isVoted = false;

    public static event Action OnVoted;

    private void Awake()
    {
        _openButton.onClick.AddListener(VoteWindow);
    }
    private void Update()
    {
        if (_isVote)
        {
            _voteCurrentTime += Time.deltaTime;
            Voting();
        }
           
    }

    private void VoteWindow()
    {
        if(_isOpen)
            CloseVote();
        else
            OpenVote();
    }
    private void OpenVote()
    {
        _isOpen = true;
        Debug.Log($"OpenVote : {_isOpen}");
        transform.DOLocalMoveX(298f, _duration);
        StartCoroutine(VoteCoroutine(_duration));
    }
    private void CloseVote()
    {
        Debug.Log($"CloseVote : {_isOpen}");
        _isOpen = false;
        transform.DOLocalMoveX(502f, _duration);
        StartCoroutine(VoteCoroutine(_duration));
    }
    private IEnumerator VoteCoroutine(float duration)
    {
        _openButton.onClick.RemoveAllListeners();
        yield return new WaitForSeconds(duration);
        _openImage.transform.rotation = Quaternion.Euler(0f, 0f, _isOpen ? 0f : 180f);
        _openButton.onClick.AddListener(VoteWindow);
    }
    private void Voting()
    {
        _voteButton.GetComponent<Image>().fillAmount = _voteCurrentTime / _voteMaxTime;
        if (_voteCurrentTime > _voteMaxTime)
        {
            _voteButton.GetComponent<Image>().fillAmount = _currentFillAmount;
            OnVoteServerRpc();
            _isVote = false;
            _isVoted = true;
        }
    }
    public void VoteButtonDown()
    {
        if(!_isVoted)
            _isVote = true;
    }
    public void VoteButtonUp()
    {
        if(!_isVote && !_isVoted)
            _voteButton.GetComponent<Image>().fillAmount = 0f;
        _isVote = false;
        _voteCurrentTime = 0f;
    }
    private void Voted()
    {
        _currentFillAmount += 0.5f;
        _voteButton.GetComponent<Image>().fillAmount += _currentFillAmount;
        if(_currentFillAmount >= 1f)
        {
            NetworkManager.SceneManager.LoadScene("Case1", LoadSceneMode.Single);
            OnVoted?.Invoke();
        }
    }
    [ServerRpc(RequireOwnership = false)]
    private void OnVoteServerRpc()
    {
        OnVoteClientRpc();
    }
    [ClientRpc]
    private void OnVoteClientRpc()
    {
        Voted();
    }
}

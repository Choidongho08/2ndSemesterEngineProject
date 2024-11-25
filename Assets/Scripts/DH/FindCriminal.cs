using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindCriminal : MonoBehaviour
{
    [SerializeField] private Button _goFindButton;
    [SerializeField] private Button _windowButton;

    private Vector3 _windowPosition;
    private bool _isOpenWindow = false;

    private void Awake()
    {
        _windowButton.onClick.AddListener(WindowOpening);
        _windowPosition = gameObject.transform.position;
    }

    private void WindowOpening()
    {
        if(!_isOpenWindow)
        {
            _isOpenWindow = true; // ø≠æÓ¡‹

            gameObject.transform.DOKill();
            gameObject.transform.DOMoveX(2150f, 0.3f);
            _windowButton.GetComponentInChildren<Image>().transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            _isOpenWindow = false; // ¥›æ∆¡‹

            gameObject.transform.DOKill();
            gameObject.transform.DOMoveX(2810f, 0.3f);
            _windowButton.GetComponentInChildren<Image>().transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}

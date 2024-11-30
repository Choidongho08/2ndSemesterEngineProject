using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectCriminal : MonoBehaviour
{
    [SerializeField] private List<GameObject> _charL; // ĳ���� �г�
    [SerializeField] private List<GameObject> _charI; // ĳ���� �̹��� �г�

    [SerializeField] private GameObject _objCurrentPanel;

    public Transform _draggablesTransform;
    public Transform _lRestTransform;
    public Transform _rRestTransform;

    public GameObject SelectEnvidence;
    public GameObject CharImage;

    public int _currentIndex = 0; // ���� ĳ���� �ε���;

    private void Start()
    {
        // �ʱ� ��ġ ����
        UpdateCharPosi();

        SelectEnvidence?.SetActive(false);
    }

    private void UpdateCharPosi()
    {
        for (int i = 0; i < _charL.Count; i++)
        {
            if (i == _currentIndex) // ���� ĳ���� �߾� ��ġ
            {
                _charL[i].transform.position = _draggablesTransform.position;
                _charL[i].SetActive(true);
            }
            else if (i == (_currentIndex - 1 + _charL.Count) % _charL.Count) // ���� ĳ���� ���� ��ġ
            {
                _charL[i].transform.position = _lRestTransform.position;
                _charL[i].SetActive(true);
            }
            else if (i == (_currentIndex + 1) % _charL.Count) // ���� ĳ���� ������ ��ġ
            {
                _charL[i].transform.position = _rRestTransform.position;
                _charL[i].SetActive(true);
            }
            else // �������� ĳ���� ȭ�鿡�� ������ �ʴ� ��ġ��
            {
                _charL[i].transform.position = _rRestTransform.position; // ȭ�� ��
                _charL[i].SetActive(false);
            }
        }
    }

    public void MoveLeft()
    {
        Debug.Log("Left");

        _currentIndex = (_currentIndex - 1 + _charL.Count) % _charL.Count;
        UpdateCharPosi();
        Debug.Log("Current Index : " + _currentIndex);
    }

    public void MoveRight()
    {
        Debug.Log("Right");

        _currentIndex = (_currentIndex + 1) % _charL.Count;
        UpdateCharPosi();
        Debug.Log("Current Index : " + _currentIndex);
    }

    public void Proposal()
    {
        SelectEnvidence?.SetActive(true);

        for (int i = 0; i < _charL.Count; i++)
        {
            if (_charL[i].transform.position == _draggablesTransform.position)
            {
                _objCurrentPanel = _charL[i];
                Debug.Log("Load Select Criminal " + _charL[i]);
                CharImage.GetComponent<Image>().sprite = _charL[i].GetComponent<Image>().sprite;
                CharImage.GetComponent<RectTransform>().sizeDelta = _charI[i].GetComponent<RectTransform>().sizeDelta;
                CharImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(_charI[i].GetComponent<RectTransform>().anchoredPosition.x, 
                    _charI[i].GetComponent<RectTransform>().anchoredPosition.y);
            }
        }
    }
}

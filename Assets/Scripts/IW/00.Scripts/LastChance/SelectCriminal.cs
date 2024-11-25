using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCriminal : MonoBehaviour
{
    [SerializeField] private List<GameObject> _charL;

    public Transform _draggablesTransform;
    public Transform _lRestTransform;
    public Transform _rRestTransform;

    public int _currentIndex = 0; // 현재 캐릭터 인덱스

    private void Start()
    {
        // 초기 위치 설정
        UpdateCharPosi();
    }

    private void UpdateCharPosi()
    {
        for (int i = 0; i < _charL.Count; i++)
        {
            if (i == _currentIndex) // 현재 캐릭터 중앙 배치
            {
                _charL[i].transform.position = _draggablesTransform.position;
                _charL[i].SetActive(true);
            }
            else if (i == (_currentIndex - 1 + _charL.Count) % _charL.Count) // 이전 캐릭터 왼쪽 배치
            {
                _charL[i].transform.position = _lRestTransform.position;
                _charL[i].SetActive(true);
            }
            else if (i == (_currentIndex + 1) % _charL.Count) // 다음 캐릭터 오른쪽 배치
            {
                _charL[i].transform.position = _rRestTransform.position;
                _charL[i].SetActive(true);
            }
            else // 나머지는 캐릭터 화면에서 보이지 않는 위치로
            {
                _charL[i].transform.position = _rRestTransform.position; // 화면 밖
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
        Debug.Log("Select Criminal");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCriminal : MonoBehaviour
{
    [SerializeField] private List<GameObject> _charL;

    public int _currentIndex = 0; // 현재 캐릭터 인덱스

    public void MoveLeft()
    {
        Debug.Log("Left");

        if (_currentIndex == 0)
        {
            Debug.Log("You couldn't move Left");
            return;
        }
        if (_currentIndex < 0)
        {
            _currentIndex = 0;
            return;
        }

        if (_currentIndex >= 0 && _currentIndex <= _charL.Count)
        {
            _charL[_currentIndex].transform.position = _charL[_currentIndex - 1].transform.position;
            
            for (int i = 0; i < _charL.Count; i++)
            {
                _charL[i].transform.position = _charL[i - 1].transform.position;
            }

        }

        _currentIndex--;
    }

    public void MoveRight()
    {
        Debug.Log("Right");

        if (_currentIndex <= _charL.Count)
        {
            Debug.Log("You couldn't move Right");
            return;
        }
        if (_currentIndex >= _charL.Count)
        {
            _currentIndex = _charL.Count;
            return;
        }

        if (_currentIndex >= 0 && _currentIndex < _charL.Count)
        {
            // _charL[_currentIndex].transform.position = _charL[_currentIndex + 1].transform.position;

            for (int i = 0; i < _charL.Count; i++)
            {
                _charL[i].transform.position = _charL[i + 1].transform.position;
            }
        }

        _currentIndex++;
    }
}

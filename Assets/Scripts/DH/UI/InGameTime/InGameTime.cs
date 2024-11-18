using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class InGameTime : MonoBehaviour
{
    [SerializeField] private Image _timeSprite;
    [SerializeField] private TimeSpriteSO _timeSpriteSo;
    [SerializeField] private int _minute;

    private int _timeCount = 0;

    private void Start()
    {
        TimeStart();
    }

    private void TimeStart()
    {
        StartCoroutine(TimeCoroutine());
    }
    private IEnumerator TimeCoroutine()
    {
        yield return new WaitForSecondsRealtime(_minute * 60);
        _timeCount++;
        if (_timeCount >= 6)
            _timeSprite.sprite = _timeSpriteSo.timeSprites[_timeCount = 0];
        else
            _timeSprite.sprite = _timeSpriteSo.timeSprites[_timeCount];
        StartCoroutine(TimeCoroutine());
    }
}

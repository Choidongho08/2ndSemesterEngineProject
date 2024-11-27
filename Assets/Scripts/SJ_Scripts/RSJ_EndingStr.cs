using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class RSJ_EndingStr : MonoBehaviour
{
    [SerializeField] private GameObject backGround; // ���
    [SerializeField] private List<Sprite> storyImage;
    [SerializeField] private GameObject EndingPan; // ���� ���丮 �� ������ ���ð�
    [TextArea]
    [SerializeField] private List<string> _storyList;
    [SerializeField] private TextMeshProUGUI _text;

    private int _storyLine = 0;

    private void Awake()
    {
        backGround.GetComponent<Image>().sprite = storyImage[0];
        _text.text = _storyList[_storyLine];
    }

    public void NextStory()
    {
        _storyLine++;
        try
        {
            _text.text = _storyList[_storyLine];
        }
        catch (Exception)
        {
            EndStory();
        }
    }
    public void EndStory()
    {
        _storyLine = 0;
        _text.text = _storyList[_storyLine];
        backGround.SetActive(false);
        EndingPan.SetActive(true);
    }
}

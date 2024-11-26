using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RSJ_EndingStr : MonoBehaviour
{
    [SerializeField] private GameObject backGround; // 배경
    [SerializeField] private List<Sprite> storyImage;
    [SerializeField] private GameObject EndingPan; // 엔딩 스토리 다 지나고 나올거
    [TextArea]
    [SerializeField] private List<string> _storyList;
    [SerializeField] private TextMeshProUGUI _text;

    private int _storyLine = 0;

    private void Awake()
    {
        backGround.GetComponent<Image>().sprite = storyImage[0];
        _text.text = _storyList[_storyLine];
    }

    private void Start()
    {
        _text.text = _text.text.Replace("\\n", "\n");
    }

    public void NextStory()
    {
        _storyLine++;
        if (_storyLine != _storyList.Count)
        {
            _text.text = _storyList[_storyLine];
            ChangeBackGround();
        }
        else
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

    private void ChangeBackGround()
    {
        if (_storyLine == 2)
            backGround.GetComponent<Image>().sprite = storyImage[1];
        else if (_storyLine == 4)
            backGround.GetComponent<Image>().sprite = storyImage[2];
        else if (_storyLine == 6)
            backGround.GetComponent<Image>().sprite = storyImage[3];
        else if (_storyLine == 7)
            backGround.GetComponent<Image>().sprite = storyImage[0];
    }
}

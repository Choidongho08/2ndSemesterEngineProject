using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class RadioGimick : PuzzleManager
{
    [SerializeField] private GameObject _radio;
    [SerializeField] private TextMeshProUGUI _nowChannel;
    public void ChannelUP()
    {
        if (_gimick.radioChannel != 5)
        {
            _gimick.radioChannel++;
            SetChannel();
        }
    }
    public void ChannelDown()
    {
        if (_gimick.radioChannel != 1)
        {
            _gimick.radioChannel--;
            SetChannel();
        }
    }

    public void OpenRadio()
    {
        _radio.SetActive(true);
    }

    public void CloseRadio()
    {
        _radio.SetActive(false);
    }

    private void SetChannel()
    {
        _nowChannel.text = _gimick.radioChannel.ToString();
        //사운드 틀기
    }
}

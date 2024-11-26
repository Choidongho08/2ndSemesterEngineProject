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
    private int[] passarr = new int[5];

    private void Awake()
    {
        for (int i = 0; i < _gimick.radioGimickPass.Length; i++)
        {
            passarr[i] = int.Parse(_gimick.radioGimickPass[i].ToString());
        }
    }

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
        Debug.Log(passarr[_gimick.radioChannel - 1]);
        AudioClip audio = _gimick.sound[passarr[_gimick.radioChannel - 1]];
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(audio);
    }
}

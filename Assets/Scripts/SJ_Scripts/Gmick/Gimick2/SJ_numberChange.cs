using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SJ_numberChange : PuzzleManager
{
    [SerializeField] private TextMeshProUGUI number1;
    [SerializeField] private TextMeshProUGUI number2;
    [SerializeField] private TextMeshProUGUI number3;
    [SerializeField] private TextMeshProUGUI number4;

    TextMeshProUGUI[] tmp;

    private void Awake()
    {
        tmp = new TextMeshProUGUI[] { number1, number2, number3, number4 };
        if (!_gimick.puzzleSet)
        {
            _gimick.flickPuzzlePassword = Random.Range(1000, 10000).ToString();
        }
    }

    private void Start()
    {
        if (!_gimick.puzzleSet)
        {
            for (int i = 0; i < 4; i++)
            {
                _gimick.numberCheck += ChangeMosuSignal(_gimick.flickPuzzlePassword[i].ToString());

                Debug.Log(_gimick.numberCheck);
            }
            _gimick.puzzleSet = true;
        }
        for(int i = 0; i < 4; i++)
            tmp[i].text = _gimick.flickPuzzlePassword[i].ToString();
    }

    private string ChangeMosuSignal(string str)
    {
        #region 숫자 모스부호 전환
        switch (str)
        {
            case "0":
            str = "11111";
                break;
            case "1":
                str = "01111";
                break;
            case "2":
                str = "00111";
                break;
            case "3":
                str = "00011";
                break;
            case "4":
                str = "00001";
                break;
            case "5":
                str = "00000";
                break;
            case "6":
                str = "10000";
                break;
            case "7":
                str = "11000";
                break;
            case "8":
                str = "11100";
                break;
            case "9":
                str = "11110";
                break;
        }
        return str;
        #endregion
    }
}

using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameTime : MonoBehaviour
{
    [SerializeField] private Image _timeSprite;
    [SerializeField] private TimeSpriteSO _timeSpriteSo;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private int _minute;
    [SerializeField] private int _year, _month, _day;

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
        {
            _timeSprite.sprite = _timeSpriteSo.timeSprites[_timeCount = 0];
            OneDay();
        }
        else
            _timeSprite.sprite = _timeSpriteSo.timeSprites[_timeCount];
        StartCoroutine(TimeCoroutine());
    }
    private void OneDay()
    {
        _day++;
        if (_day >= GetMonthOfDay(GetMonth(_month)))
        {
            _day = 1;
            OneMonth();
            return;
        }
        SetText();
    }
    private void OneMonth()
    {
        _month++;
        if (_month >= 13)
        {
            _month = 1;
            OneYear();
            return;
        }
        SetText();
    }
    private void OneYear()
    {
        _year++;
        SetText();
        return;
    }
    private void SetText()
    {
        _timeText.text = $"{_year}|{_month}|{_day}";
    }
    private int LeapYearCommonYear()
    {
        if (_year % 4 == 0 && _year % 100 != 0 || _year % 400 == 0)
            return 29;
        return 28;
    }
    private int GetMonthOfDay(MonthOfDay monthOfDay) => monthOfDay switch
    {
        MonthOfDay.January => 31,
        MonthOfDay.February => LeapYearCommonYear(),
        MonthOfDay.March => 31,
        MonthOfDay.Aprill => 30,
        MonthOfDay.May => 31,
        MonthOfDay.June => 30,
        MonthOfDay.July => 31,
        MonthOfDay.Augest => 31,
        MonthOfDay.September => 30,
        MonthOfDay.October => 31,
        MonthOfDay.November => 30,
        MonthOfDay.December => 31,
        _ => throw new Exception("Month was not Defined ")
    };
    private MonthOfDay GetMonth(int month) => month switch
    {
        1 => MonthOfDay.January,
        2 => MonthOfDay.February,
        3 => MonthOfDay.March,
        4 => MonthOfDay.Aprill,
        5 => MonthOfDay.May,
        6 => MonthOfDay.June,
        7 => MonthOfDay.July,
        8 => MonthOfDay.Augest,
        9 => MonthOfDay.September,
        10 => MonthOfDay.October,
        11 => MonthOfDay.November,
        12 => MonthOfDay.December,
        _ => throw new Exception("Month was not Defined ")
    };
}

public enum MonthOfDay
{
    January,
    February,
    March,
    Aprill,
    May,
    June,
    July,
    Augest,
    September,
    October,
    November,
    December,
}

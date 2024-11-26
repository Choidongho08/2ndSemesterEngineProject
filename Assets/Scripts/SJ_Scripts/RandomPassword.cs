using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPassword : MonoBehaviour
{
    [SerializeField] private GimickSO Gimick;

    public void StatuePasswordSet(ItemSO iteminfo)
    {
        iteminfo.ItemInfo = Gimick.StatuGimickPass + " 이라는 숫자가 적혀져 있다.";
    }

    public void RadioPasswordSet(ItemSO iteminfo)
    {
        iteminfo.ItemInfo = Gimick.radioGimickPass + " 이라는 숫자가 적혀져 있다.";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPassword : MonoBehaviour
{
    [SerializeField] private GimickSO Gimick;

    public void StatuePasswordSet(ItemSO iteminfo)
    {
        iteminfo.ItemInfo = Gimick.StatuGimickPass + " �̶�� ���ڰ� ������ �ִ�.";
    }

    public void RadioPasswordSet(ItemSO iteminfo)
    {
        iteminfo.ItemInfo = Gimick.radioGimickPass + " �̶�� ���ڰ� ������ �ִ�.";
    }
}

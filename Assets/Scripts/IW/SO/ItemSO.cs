using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Item So", order = 0)]
public class ItemSO : ScriptableObject
{
    [Header("아이템 정보")]
    public string ItemName;

    [Header("아이템 세부 정보")]
    public bool OnItem = false;
}

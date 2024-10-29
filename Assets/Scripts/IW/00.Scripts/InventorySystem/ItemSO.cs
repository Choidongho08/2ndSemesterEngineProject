using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Item So", order = 0)]
public class ItemSO : ScriptableObject
{
    [Header("아이템 정보")]
    public string ItemName; // 아이템의 고유 ID
    public string ItemInfo; // 아이템 설명
    public Sprite ItemIcon; // 아이템 그림

    [Header("플레이어 상호작용")]
    public string[] PlayerID;
    internal Sprite sprite;
}

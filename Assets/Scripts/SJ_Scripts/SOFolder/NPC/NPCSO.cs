using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/NPC")]
public class NPCSO : ScriptableObject
{
    [Header("캐릭 정보")]
    public string CharName; // 캐릭터 이름
    public Sprite CharImg; // 캐릭터 이미지
}

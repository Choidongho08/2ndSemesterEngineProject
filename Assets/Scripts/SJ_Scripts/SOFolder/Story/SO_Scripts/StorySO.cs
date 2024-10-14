using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "SO/NPCSO")]
public class StorySO : ScriptableObject
{
    [Header("캐릭 정보")]
    public string CharName; // 캐릭터 이름
    public Sprite CharImg; // 캐릭터 이미지

    [Header("적당한 대사 모음집")]
    public List<string> ChaTxts; // 캐릭터 일반 대사

    [Header("NPC역활")]
    public bool isthisNPCImportant; // 증거에 관한 질문, 심문 가능
    public bool isthisNPCcanQnA; // 증거에 관한 질문

    [Header("임시긴 한데 NPC역활에 따른 대사 관련 변화(QnA,Important)")]
    public List<string> ActEvidence; // 캐릭터가 알만한 증거 관련 
    public List<string> NoneActTxts; // ActEvidence에 없으면 말할 거
    public List<string> ActTxts; //있으면 말할 거
    public int stopPoint; // 선택지 멈춤 포인트
    public string[] PlayerAndNPC = new string[2]; // 플레이어, 그 NPC 이름

    [Header("이것도 임시지만 범인 찾을 때 제시해야하는 증거(Important)")]
    public List<string> CorrectEvidence; //맞는 증거
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StorySO/EviTexts")]
public class EvidenceTextSO : ScriptableObject
{
    [Header("임시긴 한데 NPC역활에 따른 대사 관련 변화(QnA,Important)")]
    public List<ItemSO> ActEvidence; // 캐릭터가 알만한 증거 관련 
    public StoryTxtSO NoneActTxts; // ActEvidence에 없으면 말할 거
    public List<StoryTxtSO> ActTxts; //있으면 말할 거
    public int stopPoint; // 선택지 멈춤 포인트
    public string NPC; // 플레이어, 그 NPC 이름

    [Header("이것도 임시지만 범인 찾을 때 제시해야하는 증거(Important)")]
    public List<ItemSO> CorrectEvidence; //맞는 증거
    public StoryTxtSO WrrongEvidenceText; // CorrectEvidence에 없으면 말할 거
    public List<StoryTxtSO> CorrectEvidencText; //있으면 말할 거
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StorySO/Texts")]
public class StoryTxtSO : ScriptableObject
{
    [Header("적당한 대사 틀")]
    public List<string> ChaTxts; // 캐릭터 일반 대사
    public int stopPoint; // 선택지 멈춤 포인트(없으면 대강 큰수 대충 10000000 넣기)
}

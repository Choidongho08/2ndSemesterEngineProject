using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gimick/SO")]
public class GimickSO : ScriptableObject
{
    [Header("전구 쪽 퍼즐용")]
    public string flickPuzzlePassword; // 걍 비번
    public bool puzzleSet = false; // 설정되면 다시는 못 바꾸게 하는거
    public string numberCheck; // 모스부호 전환된 비번 (입력이 되도록 변환)
    public bool puzzleEnd = false; // 퍼즐 해결

    [Header("집 덩굴 쪽 기믹")]
    public bool itCantEnter = false; // true면 이미지 바꾸고 들어갈 수 있게
    public bool player1HasScissor = false; // 플레이어 나누기 ㅇㅇ
    public bool player2HasScissor = false; // 플레이어 나누기 ㅇㅇ

    [Header("동상 우산 쪽 기믹")]
    public bool player1HasUm = false; // true면 우산을 놔둘지 선택
    public bool player2HasWater = false; // true면 물을 부울지 말지 선택
    public bool player2Watering = false; // true면 이미 물을 부운 상황
    public bool StatueHasUm = false; // 우산 가지고 있는지 (아래에 StatBlockR머시기와 연결)
    public bool StatueBlockRain = false; // 비가 내려서 막고 있는지

    [Header("라디오, 스피커 쪽 기믹")]
    [Range(1,5)] public int radioChannel; // 수에 따라서 나오는 소리가 다름
    public List<AudioSource> sound; // 수에 따른 나올 소리
    public bool puzzleDone = false; // 퍼즐이 해결 되었는지

    [Header("비번들")]
    public string radioGimickPass; //라디오 기믹 비밀번호
    public string StatuGimickPass; //동상 기믹 비밀번호
    public string RadioPassWord; //라디오 기믹 비밀번호 입력
    public string StatuPassWord; //동상 기믹 비밀번호 입력
}

public interface ItemCheck
{
    void ItemCheck();
}

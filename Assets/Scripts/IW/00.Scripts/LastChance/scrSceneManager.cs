using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scrSceneManager : MonoBehaviour
{
    public GameObject suggestEvidencePrefab; // 프리팹을 코드에서 생성하려면 프리팹을 Inspector에서 연결

    private void OnEnable()
    {
        // 씬 로드 시마다 처리하는 메서드
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // 씬 로드 이벤트 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "LastChance")
        {
            LoadSuggestEvidence();
        }
        else
        {
            // 다른 씬에서는 scrSuggestEvidence를 제거
            if (scrSuggestEvidence.Instance != null)
            {
                Destroy(scrSuggestEvidence.Instance.gameObject);
                Debug.Log("Removed scrSuggestEvidence in non-LastChance scene.");
            }
        }
    }

    void LoadSuggestEvidence()
    {
        if (scrSuggestEvidence.Instance == null)
        {
            // 1. scrSuggestEvidence 직접 생성
            GameObject suggestEvidenceObject = new GameObject("scrSuggestEvidence");

            suggestEvidenceObject.AddComponent<scrSuggestEvidence>(); // scrSuggestEvidence 스크립트 추가
            RectTransform rectTransform = suggestEvidenceObject.AddComponent<RectTransform>(); // RectTransform 추가
            rectTransform.SetParent(GameObject.Find("Canvas").transform, false); // Canvas 아래에 위치

            if (suggestEvidencePrefab != null)
            {
                Instantiate(suggestEvidencePrefab, rectTransform); // 프리팹을 Canvas에 동적 생성
            }

            Debug.Log("scrSuggestEvidence dynamically created.");
        }
    }
}

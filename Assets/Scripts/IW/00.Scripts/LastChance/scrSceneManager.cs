using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scrSceneManager : MonoBehaviour
{
    // 프리팹 없이 동적으로 생성할 스크립트들
    void LoadSuggestEvidence()
    {
        if (SceneManager.GetActiveScene().name == "LastChance")
        {
            // scrSuggestEvidence가 없으면 새로 생성
            if (scrSuggestEvidence.Instance == null)
            {
                GameObject suggestEvidenceObject = new GameObject("scrSuggestEvidence");
                suggestEvidenceObject.AddComponent<scrSuggestEvidence>(); // scrSuggestEvidence 스크립트 추가
                RectTransform rectTransform = suggestEvidenceObject.AddComponent<RectTransform>(); // RectTransform 컴포넌트 추가
                rectTransform.SetParent(GameObject.Find("Canvas").transform, false); // Canvas에 자식으로 추가
                Debug.Log("scrSuggestEvidence dynamically created.");
            }
        }
        else
        {
            // 다른 씬에서는 scrSuggestEvidence 삭제
            if (scrSuggestEvidence.Instance != null)
            {
                Destroy(scrSuggestEvidence.Instance.gameObject);
                Debug.Log("Removed scrSuggestEvidence in non-LastChance scene.");
            }
        }
    }

    void LoadSelectEvidence()
    {
        if (SceneManager.GetActiveScene().name == "LastChance")
        {
            // scrSelectEvidence가 없으면 새로 생성
            if (scrSelectEvidence.Instance == null)
            {
                GameObject selectEvidenceObject = new GameObject("scrSelectEvidence");
                selectEvidenceObject.AddComponent<scrSelectEvidence>(); // scrSelectEvidence 스크립트 추가
                RectTransform rectTransform = selectEvidenceObject.AddComponent<RectTransform>(); // RectTransform 컴포넌트 추가
                rectTransform.SetParent(GameObject.Find("Canvas").transform, false); // Canvas에 자식으로 추가
                Debug.Log("scrSelectEvidence dynamically created.");
            }
        }
        else
        {
            // 다른 씬에서는 scrSelectEvidence 삭제
            if (scrSelectEvidence.Instance != null)
            {
                Destroy(scrSelectEvidence.Instance.gameObject);
                Debug.Log("Removed scrSelectEvidence in non-LastChance scene.");
            }
        }
    }

    // 씬이 로드될 때마다 필요한 스크립트들을 로드
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadSuggestEvidence();
        LoadSelectEvidence();
    }

    // 씬이 로드될 때 이벤트 등록
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 씬 언로드 시 이벤트 제거
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

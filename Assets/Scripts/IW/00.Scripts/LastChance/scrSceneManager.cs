using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scrSceneManager : MonoBehaviour
{
    // 씬 전환 시 필요한 동적 생성 코드
    void LoadSuggestEvidence()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        // "Case1World1" 또는 "LastChance" 씬에서만 동적으로 생성
        if (sceneName == "Case1World1" || sceneName == "Case1World2" || sceneName == "LastChance")
        {
            // scrSuggestEvidence 생성
            if (scrSuggestEvidence.Instance == null)
            {
                GameObject suggestEvidenceObject = new GameObject("scrSuggestEvidence");
                suggestEvidenceObject.AddComponent<scrSuggestEvidence>();

                RectTransform rectTransform = suggestEvidenceObject.AddComponent<RectTransform>();
                rectTransform.SetParent(GameObject.Find("Canvas").transform, false);
                Debug.Log("scrSuggestEvidence dynamically created.");
            }

            // scrSelectEvidence 생성
            if (scrSelectEvidence.Instance == null)
            {
                GameObject selectEvidenceObject = new GameObject("scrSelectEvidence");
                selectEvidenceObject.AddComponent<scrSelectEvidence>();

                RectTransform rectTransform = selectEvidenceObject.AddComponent<RectTransform>();
                rectTransform.SetParent(GameObject.Find("Canvas").transform, false);
                Debug.Log("scrSelectEvidence dynamically created.");
            }
        }
        else
        {
            // "Case1World1" 및 "Case1World2", "LastChance" 씬 외의 씬에서는 삭제
            if (scrSuggestEvidence.Instance != null)
            {
                Destroy(scrSuggestEvidence.Instance.gameObject);
                Debug.Log("Removed scrSuggestEvidence in non-target scene.");
            }

            if (scrSelectEvidence.Instance != null)
            {
                Destroy(scrSelectEvidence.Instance.gameObject);
                Debug.Log("Removed scrSelectEvidence in non-target scene.");
            }
        }
    }

    // 씬 로드가 완료되었을 때 호출되는 함수
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 씬이 로드될 때마다 호출되는 이벤트 핸들러
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadSuggestEvidence();  // 씬이 변경되었을 때마다 동적 생성 및 삭제 처리
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scrSceneManager : MonoBehaviour
{
    // �� ��ȯ �� �ʿ��� ���� ���� �ڵ�
    void LoadSuggestEvidence()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        // "Case1World1" �Ǵ� "LastChance" �������� �������� ����
        if (sceneName == "Case1World1" || sceneName == "Case1World2" || sceneName == "LastChance")
        {
            // scrSuggestEvidence ����
            if (scrSuggestEvidence.Instance == null)
            {
                GameObject suggestEvidenceObject = new GameObject("scrSuggestEvidence");
                suggestEvidenceObject.AddComponent<scrSuggestEvidence>();

                RectTransform rectTransform = suggestEvidenceObject.AddComponent<RectTransform>();
                rectTransform.SetParent(GameObject.Find("Canvas").transform, false);
                Debug.Log("scrSuggestEvidence dynamically created.");
            }

            // scrSelectEvidence ����
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
            // "Case1World1" �� "Case1World2", "LastChance" �� ���� �������� ����
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

    // �� �ε尡 �Ϸ�Ǿ��� �� ȣ��Ǵ� �Լ�
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // ���� �ε�� ������ ȣ��Ǵ� �̺�Ʈ �ڵ鷯
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadSuggestEvidence();  // ���� ����Ǿ��� ������ ���� ���� �� ���� ó��
    }
}

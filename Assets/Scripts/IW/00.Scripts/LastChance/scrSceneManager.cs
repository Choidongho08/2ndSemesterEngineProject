using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scrSceneManager : MonoBehaviour
{
    public GameObject suggestEvidencePrefab; // �������� �ڵ忡�� �����Ϸ��� �������� Inspector���� ����

    private void OnEnable()
    {
        // �� �ε� �ø��� ó���ϴ� �޼���
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // �� �ε� �̺�Ʈ ����
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
            // �ٸ� �������� scrSuggestEvidence�� ����
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
            // 1. scrSuggestEvidence ���� ����
            GameObject suggestEvidenceObject = new GameObject("scrSuggestEvidence");

            suggestEvidenceObject.AddComponent<scrSuggestEvidence>(); // scrSuggestEvidence ��ũ��Ʈ �߰�
            RectTransform rectTransform = suggestEvidenceObject.AddComponent<RectTransform>(); // RectTransform �߰�
            rectTransform.SetParent(GameObject.Find("Canvas").transform, false); // Canvas �Ʒ��� ��ġ

            if (suggestEvidencePrefab != null)
            {
                Instantiate(suggestEvidencePrefab, rectTransform); // �������� Canvas�� ���� ����
            }

            Debug.Log("scrSuggestEvidence dynamically created.");
        }
    }
}

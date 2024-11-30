using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scrSceneManager : MonoBehaviour
{
    // ������ ���� �������� ������ ��ũ��Ʈ��
    void LoadSuggestEvidence()
    {
        if (SceneManager.GetActiveScene().name == "LastChance")
        {
            // scrSuggestEvidence�� ������ ���� ����
            if (scrSuggestEvidence.Instance == null)
            {
                GameObject suggestEvidenceObject = new GameObject("scrSuggestEvidence");
                suggestEvidenceObject.AddComponent<scrSuggestEvidence>(); // scrSuggestEvidence ��ũ��Ʈ �߰�
                RectTransform rectTransform = suggestEvidenceObject.AddComponent<RectTransform>(); // RectTransform ������Ʈ �߰�
                rectTransform.SetParent(GameObject.Find("Canvas").transform, false); // Canvas�� �ڽ����� �߰�
                Debug.Log("scrSuggestEvidence dynamically created.");
            }
        }
        else
        {
            // �ٸ� �������� scrSuggestEvidence ����
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
            // scrSelectEvidence�� ������ ���� ����
            if (scrSelectEvidence.Instance == null)
            {
                GameObject selectEvidenceObject = new GameObject("scrSelectEvidence");
                selectEvidenceObject.AddComponent<scrSelectEvidence>(); // scrSelectEvidence ��ũ��Ʈ �߰�
                RectTransform rectTransform = selectEvidenceObject.AddComponent<RectTransform>(); // RectTransform ������Ʈ �߰�
                rectTransform.SetParent(GameObject.Find("Canvas").transform, false); // Canvas�� �ڽ����� �߰�
                Debug.Log("scrSelectEvidence dynamically created.");
            }
        }
        else
        {
            // �ٸ� �������� scrSelectEvidence ����
            if (scrSelectEvidence.Instance != null)
            {
                Destroy(scrSelectEvidence.Instance.gameObject);
                Debug.Log("Removed scrSelectEvidence in non-LastChance scene.");
            }
        }
    }

    // ���� �ε�� ������ �ʿ��� ��ũ��Ʈ���� �ε�
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadSuggestEvidence();
        LoadSelectEvidence();
    }

    // ���� �ε�� �� �̺�Ʈ ���
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // �� ��ε� �� �̺�Ʈ ����
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

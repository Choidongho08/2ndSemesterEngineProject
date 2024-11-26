using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectCriminal : MonoBehaviour
{
    [SerializeField] private List<GameObject> _charL;

    [SerializeField] private Ease _easyType = Ease.OutBack;

    [SerializeField] private InventoryItem _invenItem;

    public Transform _draggablesTransform;
    public Transform _lRestTransform;
    public Transform _rRestTransform;

    public GameObject _inventory;

    public int _currentIndex = 0; // ���� ĳ���� �ε���

    private void Awake()
    {
        _invenItem = FindObjectOfType<InventoryItem>();
        if (_invenItem == null)
        {
            Debug.LogError("Failed to find Inven in the scene");
        }
    }

    private void Start()
    {
        if (_invenItem != null)
        {
            // ������ �߰�
            _invenItem.OnItemSelectEvent.AddListener(SelectItem);
        }
        else
        {
            Debug.LogError("InventoryItem is not assigned");
        }


        // �ʱ� ��ġ ����
        UpdateCharPosi();
    }

    private void UpdateCharPosi()
    {
        for (int i = 0; i < _charL.Count; i++)
        {
            if (i == _currentIndex) // ���� ĳ���� �߾� ��ġ
            {
                _charL[i].transform.position = _draggablesTransform.position;
                _charL[i].SetActive(true);
            }
            else if (i == (_currentIndex - 1 + _charL.Count) % _charL.Count) // ���� ĳ���� ���� ��ġ
            {
                _charL[i].transform.position = _lRestTransform.position;
                _charL[i].SetActive(true);
            }
            else if (i == (_currentIndex + 1) % _charL.Count) // ���� ĳ���� ������ ��ġ
            {
                _charL[i].transform.position = _rRestTransform.position;
                _charL[i].SetActive(true);
            }
            else // �������� ĳ���� ȭ�鿡�� ������ �ʴ� ��ġ��
            {
                _charL[i].transform.position = _rRestTransform.position; // ȭ�� ��
                _charL[i].SetActive(false);
            }
        }
    }

    public void MoveLeft()
    {
        Debug.Log("Left");

        _currentIndex = (_currentIndex - 1 + _charL.Count) % _charL.Count;
        UpdateCharPosi();
        Debug.Log("Current Index : " + _currentIndex);
    }

    public void MoveRight()
    {
        Debug.Log("Right");

        _currentIndex = (_currentIndex + 1) % _charL.Count;
        UpdateCharPosi();
        Debug.Log("Current Index : " + _currentIndex);
    }

    public void Proposal()
    {
        _inventory.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 5), 1f).SetEase(_easyType);
    }

    public void SelectItem(ItemSO itemSO)
    {
        if (itemSO != null)
        {
            Debug.Log("Event received in SelectCriminal for item " + itemSO.ItemName);
        }
        else
        {
            Debug.LogWarning("Received null ItemSO in SelectItem");
        }
    }
}

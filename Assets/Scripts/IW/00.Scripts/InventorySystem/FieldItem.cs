using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;

public class FieldItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Ease easyType = Ease.OutBack;

    [SerializeField] Vector2 _wantedPosi = new Vector2(0, -5);
    [SerializeField] private float _animTime = 1f;

    [SerializeField] private GameObject _inventory;

    private RectTransform _invenRect;
    private Vector2 _originPosi;
    Inventory Inven;

    public UnityEvent onItemGet;
    private bool _isAnimate = false;

    private void Awake()
    {
        Inven = GetComponent<Inventory>();
        _invenRect = _inventory.GetComponent<RectTransform>();

        if (_invenRect != null)
            _originPosi = _invenRect.anchoredPosition;
        else
            _originPosi = _inventory.transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !_isAnimate)
            StartCoroutine(OnInven(1f));
        else if (Input.GetKeyDown(KeyCode.F) && !_isAnimate)
            StartCoroutine(OffInven(1f));
        else if (Input.GetKeyDown(KeyCode.A))
            Inventory.Instance.GetKeyCodeA();
        else if (Input.GetKeyDown(KeyCode.D))
            Inventory.Instance.GetKeyCodeD();
    }

    public void OnAnimate()
    {
        _invenRect.DOAnchorPos(_wantedPosi, _animTime).SetEase(easyType);
        Debug.Log("인벤 열기");
    }

    public void OffAnimate()
    {
        _invenRect.DOAnchorPos(_originPosi, _animTime).SetEase(easyType);
        Debug.Log("인벤 닫기");
    }

    IEnumerator OnInven(float timeInvenAnim)
    {
        _isAnimate = true;
        OnAnimate();
        yield return new WaitForSeconds(timeInvenAnim);
        _isAnimate = false;
    }

    IEnumerator OffInven(float timeInvenAnim)
    {
        _isAnimate =true;
        OffAnimate();
        yield return new WaitForSeconds(timeInvenAnim);
        _isAnimate = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            this.gameObject.SetActive(false);
            onItemGet?.Invoke();
        }
    }
}

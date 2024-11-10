using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class FieldItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject _inventory;

    Inventory Inven;

    public UnityEvent onItemGet;

    private void Awake()
    {
        Inven = GetComponent<Inventory>();
    }

    private void Start()
    {
        _inventory.SetActive(false);
    }

    private void Update()
    {
         if (Input.GetKeyDown(KeyCode.E))
            _inventory.SetActive(true);
         else if (Input.GetKeyDown(KeyCode.F))
            _inventory.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            _inventory.SetActive(true);
            this.gameObject.SetActive(false);
            onItemGet?.Invoke();
        }
    }
}

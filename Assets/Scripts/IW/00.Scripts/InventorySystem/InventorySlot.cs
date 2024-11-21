using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public InventoryItem _myItem { get; set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Inventory._carriedItem == null) return;
            SetItem(Inventory._carriedItem);
            
        }
    }

    public void ClearSlot()
    {
        if (_myItem != null)
        {
            Destroy(_myItem.gameObject);
            _myItem = null; // ∫Û ΩΩ∑‘¿∏∑Œ √ ±‚»≠
        }
    }

    public void SetItem(InventoryItem item)
    {
        Inventory._carriedItem = null;

        // øπ¿¸ ΩΩ∑‘ ∞ªΩ≈
        item._activeSlot._myItem = null;

        // «ˆ¿Á ΩΩ∑‘ º≥¡§
        _myItem = item;
        _myItem._activeSlot = this;
        _myItem.transform.SetParent(transform);
        _myItem._canvasGroup.blocksRaycasts = true;

        //if (_myTag != SlotTag.none)
        //    Inventory.Singleton.EquipEquipment(_myTag, ItemSO);
    }
}

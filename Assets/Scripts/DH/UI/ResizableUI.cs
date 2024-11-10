using UnityEngine;
using UnityEngine.EventSystems;

public class ResizableUI : MonoBehaviour
{
    [SerializeField] private RectTransform _targetRectTransform;
    [SerializeField] private Vector2 _xSizeBoundary;
    [SerializeField] private Vector2 _ySizeBoundary;
    [SerializeField] private float _scaleFactor = 1.2f;

    
    public void OnDrag(PointerEventData eventData)
    {
        float newSizeX = _targetRectTransform.sizeDelta.x + eventData.delta.x;
        float newSizeY = _targetRectTransform.sizeDelta.y + eventData.delta.y;

        float xDelta = Mathf.Clamp(newSizeX, _xSizeBoundary.x, _xSizeBoundary.y) - _targetRectTransform.sizeDelta.x;
        float yDelta = Mathf.Clamp(newSizeX, _ySizeBoundary.x, _ySizeBoundary.y) - _targetRectTransform.sizeDelta.y;

        _targetRectTransform.sizeDelta += new Vector2(xDelta, yDelta);
        _targetRectTransform.anchoredPosition += new Vector2(xDelta, yDelta) / 2 / _scaleFactor;
    }
}

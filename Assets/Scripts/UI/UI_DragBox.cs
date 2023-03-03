using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// DragBox element of a UI window.
/// Clicking and dragging on the DragBox allows for moving the parent window around.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
public class UI_DragBox : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Min/Max x and y values the parent window can be moved to
    private const float MinX = 0;
    private float _maxX;
    private float _minY;
    private float _maxY;
    
    private RectTransform _parentTransform;
    private Vector2 _startMousePos;
    private Vector2 _startPos;
    private bool _moving;

    private void Awake()
    {
        _parentTransform = transform.parent.GetComponent<RectTransform>();
        _minY = GetComponent<RectTransform>().sizeDelta.y;
        _maxX = Screen.width - (_parentTransform.sizeDelta.x);
        _maxY = Screen.height;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _startMousePos = Input.mousePosition;
        _startPos = _parentTransform.position;
        _minY = GetComponent<RectTransform>().sizeDelta.y;
        _maxX = Screen.width - (_parentTransform.sizeDelta.x);
        _moving = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _moving = false;
    }
    
    private void Update()
    {
        if (!_moving) return;
        _parentTransform.position = new Vector2(
            Math.Min(_maxX, Math.Max(MinX, _startPos.x + (Input.mousePosition.x - _startMousePos.x))),
            Math.Min(_maxY, Math.Max(_minY, _startPos.y + (Input.mousePosition.y - _startMousePos.y))));
    }
}


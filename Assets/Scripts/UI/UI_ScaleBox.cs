using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ScaleBox element of a UI window.
/// Clicking and dragging on the ScaleBox allows for resizing of the parent window.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
public class UI_ScaleBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Texture2D cursor;
    [SerializeField] private Vector2 minWindowSize = new(50, 100);
    [SerializeField] private Vector2 step = new(0, 0);

    private RectTransform _parentTransform;
    private Vector2 _startMousePos;
    private Vector2 _startSize;
    private bool _scaling;

    void Awake()
    {
        _parentTransform = transform.parent.GetComponent<RectTransform>();
    }
    
    void Update()
    {
        if (!_scaling) return;
        _parentTransform.sizeDelta = new Vector2(
            Math.Max(_startSize.x + GetStep((Input.mousePosition.x - _startMousePos.x), step.x), minWindowSize.x),
            Math.Max(_startSize.y - GetStep((Input.mousePosition.y - _startMousePos.y), step.y), minWindowSize.y));
    }

    private float GetStep(float value, float stepValue)
    {
        if (stepValue.Equals(0)) return value;
        return (float) (Math.Round(value / stepValue) * stepValue);
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        _startMousePos = Input.mousePosition;
        _startSize = _parentTransform.sizeDelta;
        _scaling = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _scaling = false;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(cursor, new Vector2(156, 156), CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_scaling) Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}

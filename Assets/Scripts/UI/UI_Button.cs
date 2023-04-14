using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Handles button hovering and clicking actions and animations.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
public class UI_Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private Sprite hoverSprite;
    [SerializeField] private Sprite clickSprite;
    [SerializeField] private ButtonEvent buttonEvent = new ButtonEvent();
    private Image _image;
    private bool _hovered;
    private Vector3 _homePos;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _homePos = transform.position;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _hovered = true;
        _image.sprite = hoverSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _hovered = false;
        if (_image.sprite == clickSprite) transform.position = _homePos;
        _image.sprite = sprite;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _image.sprite = clickSprite;
        transform.position = _homePos - new Vector3(0, 3f, 0);
        buttonEvent.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_hovered) return;
        _image.sprite = hoverSprite;
        transform.position = _homePos;
    }

    [Serializable]
    public class ButtonEvent : UnityEvent { }
}
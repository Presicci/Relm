using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Control for ribbon menu buttons.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
public class UI_RibbonButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private GameObject controlledObject;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Color color = _image.color;
        color.a = 1f;
        _image.color = color;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Color color = _image.color;
        color.a = 0.8f;
        _image.color = color;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        controlledObject.SetActive(!controlledObject.activeInHierarchy);
    }
}

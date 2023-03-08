using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_ContextMenuEntry : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI _textMesh;
    private Action _clickFunction;

    private void Awake()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
    }

    public void SetupEntry((string, Action) entry)
    {
        _textMesh.text = entry.Item1;
        _clickFunction = entry.Item2;
        gameObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        transform.parent.GetComponent<UI_ContextMenu>().DestroyMenu();
        _clickFunction.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _textMesh.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _textMesh.color = Color.white;
    }
}
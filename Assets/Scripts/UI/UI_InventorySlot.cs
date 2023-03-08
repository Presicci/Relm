using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// UI representation of a slot in the player's inventory.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
public class UI_InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private Transform itemImageTransform;
    [SerializeField] private UI_ContextMenu contextMenu;
    private int _slotIndex;
    private Image _itemImage;
    private Item _item;

    private void Awake()
    {
        _itemImage = itemImageTransform.GetComponent<Image>();
    }

    public Item GetItem()
    {
        return _item;
    }

    public void ResetAlpha()
    {
        _itemImage.color = new Color(1, 1, 1, 0.8f);
    }

    public void SetItem(Item item)
    {
        if (item == null)
        {
            RemoveItem();
            return;
        }
        _item = item;
        _itemImage.color = new Color(1, 1, 1, 0.8f);
        _itemImage.sprite = item.GetSprite();
        _itemImage.gameObject.SetActive(true);
    }

    public void RemoveItem()
    {
        _item = null;
        _itemImage.color = new Color(1, 1, 1, 0);
        _itemImage.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_item != null)
            UI_Tooltip.Instance.ActivateItemTooltip(_item);
        UI_ItemDrag.Instance.toSlot = _slotIndex;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UI_Tooltip.Instance.DisableTooltip();
        UI_ItemDrag.Instance.toSlot = -1;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (_item == null) return;
            _itemImage.color = new Color(1, 1, 1, 0.5f);
            UI_ItemDrag.Instance.DragItem(this);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (_item == null) return;
            contextMenu.SetupMenu(Input.mousePosition, new (string, Action)[]
            {
                ("Equip", () => Debug.Log("Equip")),
                ("Use", () => Debug.Log("Use")),
                ("Inspect", () => Debug.Log("Inspect")),
                ("Discard", () => GameManager.GetPlayer().GetInventory().RemoveFromSlot(_slotIndex))
            });
        }
    }

    public int GetSlotIndex()
    {
        return _slotIndex;
    }

    public void SetSlotIndex(int value)
    {
        _slotIndex = value;
    }
}

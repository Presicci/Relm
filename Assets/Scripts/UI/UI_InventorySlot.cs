using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// UI representation of a slot in the player's inventory.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
public class UI_InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private UI_ItemDrag itemDrag;
    [SerializeField] private UI_Tooltip tooltip;
    [SerializeField] private Transform itemImageTransform;
    [SerializeField] private UI_ContextMenu contextMenu;
    [SerializeField] private TextMeshProUGUI amountTextMesh;
    [SerializeField] private Player player;
    [SerializeField] private UI_Equipment equipment;
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

        int amount = item.GetAmount();
        if (amount > 1)
        {
            SetAmount(amount);
        }
        _item = item;
        _itemImage.color = new Color(1, 1, 1, 0.8f);
        _itemImage.sprite = item.GetSprite();
        _itemImage.gameObject.SetActive(true);
    }

    public void RemoveItem()
    {
        _item = null;
        amountTextMesh.gameObject.SetActive(false);
        _itemImage.color = new Color(1, 1, 1, 0);
        _itemImage.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_item != null)
            tooltip.ActivateItemTooltip(_item);
        itemDrag.UpdateToSlot(_slotIndex);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.DisableTooltip();
        itemDrag.ResetToSlot();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (_item == null) return;
            _itemImage.color = new Color(1, 1, 1, 0.5f);
            itemDrag.DragItem(this);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (_item == null) return;
            contextMenu.SetupMenu(Input.mousePosition, new (string, Action)[]
            {
                ("Equip", () =>
                {
                    if (_item.GetEquipSlot() == EquipSlot.None) return;
                    Item item = _item;
                    player.GetInventory().RemoveFromSlot(_slotIndex);
                    equipment.EquipItem(item);
                }),
                ("Discard", () => player.GetInventory().RemoveFromSlot(_slotIndex))
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
    
    private void SetAmount(int amount)
    {
        string amt;
        if (amount / 1_000_000_000 >= 1)
        {
            amt = (amount / 1_000_000_000) + "B";
        }
        else if (amount / 1_000_000 >= 1)
        {
            amt = (amount / 1_000_000) + "M";
        }
        else if (amount / 1_000 >= 1)
        {
            amt = (amount / 1_000) + "K";
        }
        else
        {
            amt = amount + "";
        }
        amountTextMesh.gameObject.SetActive(true);
        amountTextMesh.text = amt;
        ColorAmountText(amt);
    }

    private void ColorAmountText(string amount)
    {
        if (amount.Contains("B"))
        {
            amountTextMesh.color = new Color(225, 0, 255);
        }
        else if (amount.Contains("M"))
        {
            amountTextMesh.color = new Color(0, 255, 0);
        }
        else if (amount.Contains("K"))
        {
            amountTextMesh.color = new Color(255, 255, 255);
        }
        else
        {
            amountTextMesh.color = new Color(239, 255, 0);
        }
    }
}

using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_EquipmentSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private UI_Equipment equipment;
    [SerializeField] private UI_ItemDrag itemDrag;
    [SerializeField] public EquipSlot equipSlot;
    [SerializeField] private UI_Tooltip tooltip;
    [SerializeField] private Image itemImage;
    [SerializeField] private UI_ContextMenu contextMenu;
    [SerializeField] private Player player;
    [SerializeField] private TextMeshProUGUI errorText;
    private Item _item;

    public void EquipItem(Item item)
    {
        if (item == null)
        {
            RemoveItem();
            return;
        }
        _item = item;
        itemImage.color = new Color(1, 1, 1, 0.8f);
        itemImage.sprite = item.GetSprite();
        itemImage.gameObject.SetActive(true);
        equipment.RefreshStats();
    }

    public void UnequipItem()
    {
        player.GetInventory().AddItem(_item);
        RemoveItem();
    }
    
    public void RemoveItem()
    {
        _item = null;
        itemImage.color = new Color(1, 1, 1, 0);
        itemImage.gameObject.SetActive(false);
        equipment.RefreshStats();
    }
    
    public Item GetItem()
    {
        return _item;
    }
    
    public void ResetAlpha()
    {
        itemImage.color = new Color(1, 1, 1, 0.8f);
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (_item == null) return;
            itemImage.color = new Color(1, 1, 1, 0.5f);
            itemDrag.DragItem(this);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (_item == null) return;
            contextMenu.SetupMenu(Input.mousePosition, new (string, Action)[]
            {
                ("Unequip", UnequipItem)
            });
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_item != null)
            tooltip.ActivateItemTooltip(_item);
        itemDrag.UpdateEquipmentSlot(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.DisableTooltip();
        itemDrag.ResetToSlot();
    }

    public void Error(string text)
    {
        StartCoroutine(ErrorRoutine(text));
    }

    IEnumerator ErrorRoutine(string text)
    {
        errorText.text = text;
        errorText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        errorText.gameObject.SetActive(false);
    }
}

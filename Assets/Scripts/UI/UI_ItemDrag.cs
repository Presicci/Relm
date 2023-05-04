using UnityEngine;
using UnityEngine.UI;

public delegate void OnInventoryMoveDelegate(int originalSlot, int newSlot);

/// <summary>
/// Image singleton that is created when dragging an item from the inventory.
/// Updates to follow the cursor until mouse 0 is lifted.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
public class UI_ItemDrag : MonoBehaviour
{
    // Event for updating the UI_Inventory and player Inventory
    public static event OnInventoryMoveDelegate OnMove = delegate { };

    [SerializeField] private Player player;
    private int _toSlot;
    private UI_InventorySlot _fromSlot;
    private UI_EquipmentSlot _fromEquipmentSlot;
    private UI_EquipmentSlot _toEquipmentSlot;
    private Image _image;

    private void Awake()
    {
        _image = transform.GetComponent<Image>();
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            gameObject.SetActive(false);
            if (_fromSlot != null)
                _fromSlot.ResetAlpha();
            if (_fromEquipmentSlot != null)
                _fromEquipmentSlot.ResetAlpha();
            if (_toSlot >= 0 && _fromEquipmentSlot != null)
            {
                Inventory inventory = player.GetInventory();
                Item slotItem = inventory.GetItems()[_toSlot];
                if (slotItem != null && slotItem.GetEquipSlot() == _fromEquipmentSlot.equipSlot)
                {
                    Item equippedItem = _fromEquipmentSlot.GetItem();
                    _fromEquipmentSlot.EquipItem(slotItem);
                    inventory.SetItem(equippedItem, _toSlot);
                }
                else
                {
                    inventory.SetItem(_fromEquipmentSlot.GetItem(), _toSlot);
                    _fromEquipmentSlot.RemoveItem();
                }
                return;
            }
            if (_toEquipmentSlot != null && _fromSlot != null)
            {
                Item slotItem = _fromSlot.GetItem();
                if (slotItem.GetEquipSlot() != _toEquipmentSlot.equipSlot)
                {
                    _toEquipmentSlot.Error("Wrong slot");
                    return;
                }
                if (slotItem != null && _toEquipmentSlot.GetItem() != null)
                {
                    Inventory inventory = player.GetInventory();
                    Item equippedItem = _toEquipmentSlot.GetItem();
                    _toEquipmentSlot.EquipItem(slotItem);
                    _fromSlot.SetItem(equippedItem);
                    inventory.SetItem(equippedItem, _fromSlot.GetSlotIndex());
                }
                else
                {
                    _toEquipmentSlot.EquipItem(_fromSlot.GetItem());
                    _fromSlot.RemoveItem();
                    player.GetInventory().RemoveFromSlot(_fromSlot.GetSlotIndex());
                }
                return;
            }
            if (_fromSlot == null || _toSlot < 0) return;
            int fromIndex = _fromSlot.GetSlotIndex();
            if (_toSlot >= 0 && fromIndex != _toSlot)
                OnMove.Invoke(fromIndex, _toSlot);
        }
    }

    public void UpdateEquipmentSlot(UI_EquipmentSlot equipmentSlot)
    {
        _toSlot = -1;
        _toEquipmentSlot = equipmentSlot;
    }

    public void UpdateToSlot(int toSlot)
    {
        _toSlot = toSlot;
        _toEquipmentSlot = null;
    }

    public void ResetToSlot()
    {
        _toSlot = -1;
        _toEquipmentSlot = null;
    }

    public void DragItem(UI_InventorySlot slot)
    {
        gameObject.SetActive(true);
        _fromSlot = slot;
        _fromEquipmentSlot = null;
        _image.sprite = slot.GetItem().GetSprite();
        transform.position = Input.mousePosition;
    }
    
    public void DragItem(UI_EquipmentSlot slot)
    {
        gameObject.SetActive(true);
        _fromSlot = null;
        _fromEquipmentSlot = slot;
        _image.sprite = slot.GetItem().GetSprite();
        transform.position = Input.mousePosition;
    }
}
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// UI representation of the player's inventory.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
public class UI_Inventory : MonoBehaviour
{
    [SerializeField] private TMP_InputField searchBar;
    [SerializeField] private UI_InventorySlot slotPrefab;
    private UI_InventorySlot[] _slots;
    private int _size;

    private void Start()
    {
        _size = 20;
        RebuildInventory();

        Inventory.OnAdd += OnAdd;
        Inventory.OnRemove += OnRemove;
        Inventory.OnResize += OnResize;
        UI_ItemDrag.OnMove += OnMove;

        transform.parent.parent.gameObject.SetActive(false);
    }

    private void RebuildInventory()
    {
        ResetChildren();
        if (!gameObject.activeInHierarchy) return;
        _slots = new UI_InventorySlot[_size];
        for (int index = 0; index < _size; index++)
        {
            UI_InventorySlot slot = Instantiate(slotPrefab, transform) as UI_InventorySlot;
            slot.SetSlotIndex(index);
            _slots[index] = slot;
            slot.gameObject.SetActive(true);
        }
        ReloadItems();
    }

    private void ReloadItems()
    {
        Item[] items = GameManager.GetPlayer().GetInventory().GetItems();
        for (int index = 0; index < _size; index++)
        {
            _slots[index].SetItem(items[index]);
        }
    }

    public void SearchUpdate(string value)
    {
        bool clear = string.IsNullOrEmpty(value);
        foreach (UI_InventorySlot slot in _slots)
        {
            if (clear)
            {
                slot.gameObject.SetActive(true);
                continue;
            }
            Item item = slot.GetItem();
            if (item != null && slot.GetItem().GetName().ContainsInsensitive(value))
            {
                slot.gameObject.SetActive(true);
            }
            else
            {
                slot.gameObject.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        searchBar.text = "";
        if (transform.childCount <= 0)
            RebuildInventory();
    }

    private void ResetChildren()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    /**
     * Invoked whenever an item is added to the player's inventory.
     */
    private void OnAdd(int slot, Item item)
    {
        _slots[slot].SetItem(item);
    }

    /*
     * Invoked whenever an item is removed from the player's inventory.
     */
    private void OnRemove(int slot)
    {
        _slots[slot].RemoveItem();
    }

    private void OnMove(int originalSlot, int newSlot)
    {
        Item originalItem = _slots[originalSlot].GetItem();
        _slots[originalSlot].SetItem(_slots[newSlot].GetItem());
        _slots[newSlot].SetItem(originalItem);
    }

    private void OnResize(int newSize)
    {
        _size = newSize;
        RebuildInventory();
    }
}

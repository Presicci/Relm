using System;
using UnityEngine;
using System.Linq;

public delegate void OnInventoryRemoveDelegate(int slot);
public delegate void OnInventoryAddDelegate(int slot, Item item);
public delegate void OnInventoryResizeDelegate(int newSize);

/// <summary>
/// Inventory object.
/// Manages inventory storage and interaction.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
[Serializable]
public class Inventory
{
    // Events for updating the UI_Inventory
    public static event OnInventoryRemoveDelegate OnRemove = delegate { };
    public static event OnInventoryAddDelegate OnAdd = delegate { };
    public static event OnInventoryResizeDelegate OnResize = delegate { };

    private int _size;
    private Item[] _items;

    public Inventory(int size)
    {
        _size = size;
        _items = new Item[size];
        UI_ItemDrag.OnMove += OnMoveEvent;
    }

    public void LoadInventory(Inventory inventory)
    {
        _size = inventory._size;
        _items = inventory._items;
        OnResize.Invoke(_size);
    }

    private void SetItem(Item item, int slot)
    {
        _items[slot] = item;
        OnAdd.Invoke(slot, item);
        Debug.Log("Added " + item.GetName() + " to slot " + slot);
    }

    private void UpdateSlot(int slot)
    {
        OnAdd.Invoke(slot, _items[slot]);
        Debug.Log("Added " + _items[slot].GetName() + " to slot " + slot);
    }

    public bool RemoveFromSlot(int slot)
    {
        if (_items[slot] == null)
            return false;
        _items[slot] = null;
        OnRemove.Invoke(slot);
        return true;
    }

    public bool AddItem(Item item)
    {
        return AddItem(item, item.GetAmount());
    }

    public bool AddItem(Item item, int amount)
    {
        if (item.IsStackable())
        {
            int slot = FindSlot(item);
            Item inventoryItem = slot == -1 ? null : _items[slot];
            if (inventoryItem == null)
            {
                if (!HasRoomFor(1)) return false;
                item.SetAmount(amount);
                AddItemToFirstAvailable(item);
            }
            else
            {
                inventoryItem.IncrementAmount(amount);
                UpdateSlot(slot);
            }
        }
        else
        {
            if (!HasRoomFor(amount)) return false;
            for (int i = 0; i < amount; i++)
            {
                AddItemToFirstAvailable(item);
            }
        }

        return true;
    }

    private bool AddItemToFirstAvailable(Item item)
    {
        int slot = GetFirstAvailableSlot();
        if (slot == -1)
        {
            Debug.Log("InvFull");
            return false;
        }

        SetItem(item, slot);
        return true;
    }

    public int GetFirstAvailableSlot()
    {
        for (int index = 0; index < _items.Length; index++)
        {
            if (_items[index] == null)
            {
                return index;
            }
        }
        return -1;
    }

    public bool HasRoomFor(int amount)
    {
        return GetFreeSlots() >= amount;
    }
    
    public bool HasRoomFor(Item item, int amount)
    {
        return (item.IsStackable() && Find(item) != null) || GetFreeSlots() >= amount;
    }
    
    public int GetFreeSlotsFor(Item item)
    {
        if (item.IsStackable())
        {
            Item inventoryItem = Find(item);
            return int.MaxValue - (inventoryItem?.GetAmount() ?? 0);
        }
        return _items.Count(i => i == null);
    }

    public int GetFreeSlots()
    {
        return _items.Count(i => i == null);
    }

    public bool Contains(Item item)
    {
        return Find(item) != null;
    }

    public Item Find(Item item)
    {
        return _items.FirstOrDefault(i => i != null && i.GetIdentifier().Equals(item.GetIdentifier()));
    }
    
    public int FindSlot(Item item)
    {
        for (int index = 0; index < _items.Length; index++)
        {
            if (_items[index] != null && _items[index].GetIdentifier() == item.GetIdentifier()) return index;
        }
        return -1;
    }

    public void Resize(int newSize)
    {
        // TODO handle item rearranging
        Array.Resize(ref _items, newSize);
        Debug.Log("Resized inventory to " + _items.Length);
        OnResize.Invoke(newSize);
    }

    public Item[] GetItems()
    {
        return _items;
    }

    /*
     * Invoked whenever an item is moved from one slot to another.
     */
    private void OnMoveEvent(int originalSlot, int newSlot)
    {
        (_items[originalSlot], _items[newSlot]) = (_items[newSlot], _items[originalSlot]);
        Debug.Log(ToString());
    }

    public override string ToString()
    {
        string output = "";
        for (int index = 0; index < _items.Length; index++)
        {
            output += "[" + index + ":" + (_items[index] == null ? "" : _items[index].GetName()) + "]";
        }
        return output;
    }
}
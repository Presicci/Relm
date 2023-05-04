using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Loot table object.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
public class LootTable : MonoBehaviour
{
    [SerializeField] private List<LootItem> lootItems = new();
    private int _totalWeight;

    // This makes LootItems initialize with amount and weight of 1
    private void Reset()
    {
        lootItems = new()
        {
            new LootItem()
        };
    }

    private void Awake()
    {
        foreach (var lootItem in lootItems)
        {
            _totalWeight += lootItem.weight;
        }
    }

    public Item Roll()
    {
        int roll = Random.Range(1, _totalWeight);
        foreach (var lootItem in lootItems)
        {
            if ((roll -= lootItem.weight) <= 0)
            {
                if (lootItem.item == null) return null; // No drop
                Item item = ItemDef.GetItemByName(lootItem.item.itemName);
                item.SetAmount(lootItem.amount);
                return item;
            }
        }

        Debug.LogError("Failed to roll on table for: " + transform.name);
        return null;
    }
}

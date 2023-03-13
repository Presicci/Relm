using System;
using UnityEngine;

/// <summary>
/// Item object.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
[Serializable]
public class Item
{
    private int _itemId;

    public Item(int itemId)
    {
        _itemId = itemId;
    }

    public string GetName()
    {
        return ItemDef.GetDataById(_itemId).itemName;
    }
    
    public string GetDescription()
    {
        return ItemDef.GetDataById(_itemId).itemDescription;
    }
    
    public Sprite GetSprite()
    {
        return ItemDef.GetDataById(_itemId).sprite;
    }
}
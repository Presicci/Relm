using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Item object.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
[Serializable]
public class Item
{
    private string _identifier;
    private int _amount;

    public Item(string identifier)
    {
        _identifier = identifier;
        _amount = 1;
    }
    
    public Item(string identifier, int amount)
    {
        _identifier = identifier;
        _amount = IsStackable() ? amount : 1;
    }

    public string GetIdentifier()
    {
        return _identifier;
    }

    public string GetName()
    {
        return ItemDef.GetDataByIdentifier(_identifier).itemName;
    }

    public int GetAmount()
    {
        return _amount;
    }

    public void SetAmount(int value)
    {
        _amount = value;
    }

    public void IncrementAmount(int value)
    {
        _amount += value;
    }
    
    public string GetDescription()
    {
        return ItemDef.GetDataByIdentifier(_identifier).itemDescription;
    }

    public List<ItemAffix> GetAffixes()
    {
        return ItemDef.GetDataByIdentifier(_identifier).affixes;
    }
    
    public Sprite GetSprite()
    {
        return ItemDef.GetDataByIdentifier(_identifier).sprite;
    }

    public bool IsStackable()
    {
        return ItemDef.GetDataByIdentifier(_identifier).stackable;
    }
}
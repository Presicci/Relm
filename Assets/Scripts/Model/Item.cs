using System;
using UnityEngine;

/// <summary>
/// Item object.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
[Serializable]
public class Item
{
    private string _identifier;

    public Item(string identifier)
    {
        _identifier = identifier;
    }

    public string GetName()
    {
        return ItemDef.GetDataByIdentifier(_identifier).itemName;
    }
    
    public string GetDescription()
    {
        return ItemDef.GetDataByIdentifier(_identifier).itemDescription;
    }
    
    public Sprite GetSprite()
    {
        return ItemDef.GetDataByIdentifier(_identifier).sprite;
    }
}
using UnityEngine;

/// <summary>
/// Item object.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
public class Item
{
    private readonly ItemScriptableObject _data;

    public Item(ItemScriptableObject data)
    {
        _data = data;
    }

    public string GetName()
    {
        return _data.itemName;
    }
    
    public string GetDescription()
    {
        return _data.itemDescription;
    }
    
    public Sprite GetSprite()
    {
        return _data.sprite;
    }
}
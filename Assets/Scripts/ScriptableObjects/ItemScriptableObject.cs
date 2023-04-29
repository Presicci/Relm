using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Item scriptable object.
/// Used to create item assets. 
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Item")]
public class ItemScriptableObject : ScriptableObject
{
    [Tooltip("Inventory image.")]
    public Sprite sprite;
    
    [Tooltip("Shown at the top of item tooltip.")]
    public string itemName;
    
    [Tooltip("Shown at the bottom of item tooltip.")]
    public string itemDescription;

    [Tooltip("Does the item stack?")]
    public bool stackable;

    [Tooltip("How the item modifies the player's stats.")] 
    public List<ItemAffix> affixes;
}

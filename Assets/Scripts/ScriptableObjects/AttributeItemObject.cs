using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/AttributeItem")]
public class AttributeItemObject : ScriptableObject
{
    [Tooltip("Inventory image.")]
    public Sprite sprite;
    
    [Tooltip("Shown at the top of item tooltip.")]
    public string itemName;
    
    [Tooltip("Shown at the bottom of item tooltip.")]
    public string itemDescription;

    [Tooltip("Does the item stack?")]
    public bool stackable;
}
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Shop scriptable object.
/// Used to create shop assets. 
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Shop")]
public class ShopScriptableObject : ScriptableObject
{
    public List<ShopItem> items = new List<ShopItem>();
}

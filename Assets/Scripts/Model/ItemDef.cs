using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Item database in the form of a Dictionary mapping UUIDs to Item scriptable objects.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
public class ItemDef
{
    private static Dictionary<int, ItemScriptableObject> _loadedItems;

    /// <summary>
    /// Loads item scriptable objects into a dictionary.
    /// </summary>
    public static void LoadItems()
    {
        _loadedItems = new Dictionary<int, ItemScriptableObject>();
        var count = 0;  // TODO map entries by static ids instead of dynamic ones
        string[] assetNames = AssetDatabase.FindAssets("t:" + typeof(ItemScriptableObject));
        foreach (var name in assetNames)
        {
            var path = AssetDatabase.GUIDToAssetPath(name);
            var item = AssetDatabase.LoadAssetAtPath<ItemScriptableObject>(path);
            _loadedItems.Add(count++, item);
        }
        Debug.Log("Loaded " + count + " items!");
    }

    public static Item GetById(int itemId)
    {
        return _loadedItems.ContainsKey(itemId) ? new Item(_loadedItems[itemId]) : null;
    }
}
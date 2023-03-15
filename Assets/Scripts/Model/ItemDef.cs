using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Item database in the form of a Dictionary mapping UUIDs to Item scriptable objects.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
public class ItemDef
{
    private static Dictionary<string, ItemScriptableObject> _loadedItems;

    /// <summary>
    /// Loads item scriptable objects into a dictionary.
    /// </summary>
    public static void LoadItems()
    {
        _loadedItems = new Dictionary<string, ItemScriptableObject>();
        string[] assetNames = AssetDatabase.FindAssets("t:" + typeof(ItemScriptableObject));
        foreach (var name in assetNames)
        {
            var path = AssetDatabase.GUIDToAssetPath(name);
            var item = AssetDatabase.LoadAssetAtPath<ItemScriptableObject>(path);
            _loadedItems.Add(item.itemName.ToLower().Replace(" ", "_"), item);
        }
        Debug.Log("Loaded " + _loadedItems.Count + " items!");
        UI_DevConsole.AddCommand(new("item", new List<string> { "item identifier" }, new List<List<string>> { _loadedItems.Keys.ToList() },args => GameManager.GetPlayer().GetInventory().AddItem(ItemDef.GetByIdentifier(args[0]))));
    }

    private static Item GetByIdentifier(string identifier)
    {
        return _loadedItems.ContainsKey(identifier) ? new Item(identifier) : null;
    }

    public static ItemScriptableObject GetDataByIdentifier(string identifier)
    {
        return _loadedItems.ContainsKey(identifier) ? _loadedItems[identifier] : null;
    }
    
    public static Item GetItemByName(string name)
    {
        string identifier = NameToIdentifier(name);
        return _loadedItems.ContainsKey(identifier) ? new Item(identifier) : null;
    }

    private static string NameToIdentifier(string itemName)
    {
        return itemName.ToLower().Replace(" ", "_");
    }
}
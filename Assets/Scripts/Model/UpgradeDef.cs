using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UpgradeDef
{
    public static List<UpgradeScriptableObject> LoadedUpgrades;
    
    public static void LoadUpgrades()
    {
        LoadedUpgrades = new List<UpgradeScriptableObject>();
        string[] assetNames = AssetDatabase.FindAssets("t:" + typeof(UpgradeScriptableObject));
        foreach (var name in assetNames)
        {
            var path = AssetDatabase.GUIDToAssetPath(name);
            var upgrade = AssetDatabase.LoadAssetAtPath<UpgradeScriptableObject>(path);
            LoadedUpgrades.Add(upgrade);
        }
        Debug.Log("Loaded " + LoadedUpgrades.Count + " upgrades!");
    }
}
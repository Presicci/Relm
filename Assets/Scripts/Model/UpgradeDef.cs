using System.Collections.Generic;
using UnityEditor;

public class UpgradeDef
{
    public static List<UpgradeScriptableObject> LoadedOffensiveUpgrades;
    public static List<UpgradeScriptableObject> LoadedDefensiveUpgrades;
    public static List<UpgradeScriptableObject> LoadedUtilityUpgrades;
    
    public static void LoadUpgrades()
    {
        LoadedOffensiveUpgrades = new List<UpgradeScriptableObject>();
        LoadedDefensiveUpgrades = new List<UpgradeScriptableObject>();
        LoadedUtilityUpgrades = new List<UpgradeScriptableObject>();
        string[] assetNames = AssetDatabase.FindAssets("t:" + typeof(UpgradeScriptableObject));
        foreach (var name in assetNames)
        {
            var path = AssetDatabase.GUIDToAssetPath(name);
            var upgrade = AssetDatabase.LoadAssetAtPath<UpgradeScriptableObject>(path);
            switch (upgrade.attributeClass)
            {
                case AttributeClass.Utility:
                    LoadedUtilityUpgrades.Add(upgrade);
                    break;
                case AttributeClass.Defensive:
                    LoadedDefensiveUpgrades.Add(upgrade);
                    break;
                default:
                    LoadedOffensiveUpgrades.Add(upgrade);
                    break;
            }
        }
    }
}
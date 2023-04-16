using System.Collections.Generic;
using UnityEngine;

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
        UpgradeScriptableObject[] upgrades = Resources.LoadAll<UpgradeScriptableObject>("Upgrades");
        Debug.LogError("Loaded " + upgrades.Length + " upgrades");
        foreach (var upgrade in upgrades)
        {
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
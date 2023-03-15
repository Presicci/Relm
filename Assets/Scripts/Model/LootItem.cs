using System;

/// <summary>
/// Loot item object.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
[Serializable]
public class LootItem
{
        public ItemScriptableObject item;
        public int amount;
        public int weight;

        public LootItem()
        { 
                amount = 1;
                weight = 1;
        }
}
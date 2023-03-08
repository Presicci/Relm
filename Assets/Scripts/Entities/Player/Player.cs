using System;
using UnityEngine;

/// <summary>
/// Player object.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
[Serializable]
public class Player : MonoBehaviour
{
    private Inventory _inventory;

    private void Start()
    {
        _inventory = new(20);   // Starting inventory of size 20 for now, can always be changed
    }

    public void LoadPlayer(PlayerData playerData)
    {
        _inventory.LoadInventory(playerData.inventory);
    }
    
    public Inventory GetInventory()
    {
        return _inventory;
    }
}
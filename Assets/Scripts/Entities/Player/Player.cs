using System;
using TMPro;
using UnityEngine;

/// <summary>
/// Player object.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
[Serializable]
public class Player : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldDisplay;
    private Inventory _inventory;
    public int Gold { set; get; }

    public void IncrementGold(int value)
    {
        Gold += value;
        goldDisplay.text = Gold + "";
    }
    
    private void Start()
    {
        _inventory = new(20);   // Starting inventory of size 20 for now, can always be changed
    }

    public void LoadPlayer(PlayerData playerData)
    {
        _inventory.LoadInventory(playerData.inventory);
        Gold = playerData.gold;
    }
    
    public Inventory GetInventory()
    {
        return _inventory;
    }
}
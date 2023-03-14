using UnityEngine;

/// <summary>
/// Controller for npcs or objects that open shops.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
public class ShopInteractable : Interactable
{
    [SerializeField] private ShopScriptableObject shop;
    [SerializeField] private UI_Shop shopUI;

    public override void Interact()
    {
        shopUI.OpenShop(shop);
    }

    public void CloseShop()
    {
        shopUI.CloseShop();
    }
}

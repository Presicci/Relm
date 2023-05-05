using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// UI representation for the shop that the player is browsing.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
public class UI_Shop : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private UI_ShopItem shopItemPrefab;
    [SerializeField] private UI_ShopPreview shopPreview;
    [SerializeField] private TMP_InputField customQuantityInput;
    [SerializeField] private TextMeshProUGUI previewBuyText;
    [SerializeField] private TextMeshProUGUI previewCostText;
    [SerializeField] private TextMeshProUGUI playerGold;
    [SerializeField] private Player player;

    private ShopItem _selectedItem;
    private int _quantity = 1;
    private bool _errorDisplayed;
    
    public void OpenShop(ShopScriptableObject shop)
    {
        shopPreview.gameObject.SetActive(false);
        gameObject.SetActive(true);
        ResetShopItems();
        UpdatePlayerGold();
        foreach (ShopItem item in shop.items)
        {
            UI_ShopItem shopItem = Instantiate(shopItemPrefab, container);
            shopItem.SetupItem(item, this);
        }
        Time.timeScale = 0f;
    }
    
    private void ResetShopItems()
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
    }

    public void PurchaseItem()
    {
        if (_errorDisplayed) return;
        Item item = ItemDef.GetItemByName(_selectedItem.item.itemName);
        int availableSpace = player.GetInventory().GetFreeSlotsFor(item);
        if (availableSpace <= 0)
        {
            StartCoroutine(ErrorMessage("Not enough space"));
            return;
        }
        int quantity = _quantity;
        if (quantity > availableSpace)
        {
            quantity = availableSpace;
        }
        int cost = quantity * _selectedItem.cost;
        if (player.Gold < cost)
        {
            StartCoroutine(ErrorMessage("Not enough gold"));
            return;
        }
        player.Gold -= cost;
        player.GetInventory().AddItem(item, quantity);
        UpdatePlayerGold();
    }

    private IEnumerator ErrorMessage(string message)
    {
        _errorDisplayed = true;
        previewBuyText.color = Color.red;
        previewBuyText.text = message;
        previewBuyText.fontSize = 28f;
        yield return new WaitForSecondsRealtime(2f);
        previewBuyText.color = Color.black;
        previewBuyText.fontSize = 32f;
        DisplayQuantity();
        _errorDisplayed = false;
    }
    
    private int CalculateCost()
    {
        return _quantity * _selectedItem.cost;
    }

    private void DisplayQuantity()
    {
        previewBuyText.text = "Buy " + _quantity;
        previewCostText.text = "" + CalculateCost();
    }

    public void CustomQuantity(string value)
    {
        EventSystem.current.SetSelectedGameObject(null);
        if (!int.TryParse(value, out var numericValue)) return;
        _quantity = numericValue;
        DisplayQuantity();
    }
    
    private void UpdatePlayerGold()
    {
        playerGold.text = "" + player.Gold;
        playerGold.ForceMeshUpdate();
    }

    public void CloseShop()
    {
        gameObject.SetActive(false);
    }
    
    /**
     * Button controls
     */
    public void ClickItem(ShopItem shopItem)
    {
        _selectedItem = shopItem;
        shopPreview.DisplayPreview(_selectedItem, _quantity);
    }
    
    public void IncrementQuantity()
    {
        ++_quantity;
        DisplayQuantity();
    }
    
    public void DecrementQuantity()
    {
        if (_quantity <= 1) return;
        --_quantity;
        DisplayQuantity();
    }
    
    public void ResetQuantity()
    {
        _quantity = 1;
        DisplayQuantity();
    }
    
    public void EnableCustomQuantity()
    {
        customQuantityInput.gameObject.SetActive(true);
        customQuantityInput.ActivateInputField();
    }
}

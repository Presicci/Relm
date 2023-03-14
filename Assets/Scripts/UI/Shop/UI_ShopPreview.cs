using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Sets up information on the preview element of the shop.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
public class UI_ShopPreview : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private RectTransform infoContainer;
    [SerializeField] private TextMeshProUGUI totalCost;

    public void DisplayPreview(ShopItem shopItem, int quantity)
    {
        gameObject.SetActive(true);
        ItemScriptableObject item = shopItem.item;
        itemImage.sprite = item.sprite;
        itemName.text = item.itemName;
        itemDescription.text = item.itemDescription;
        totalCost.text = "" + shopItem.cost * quantity;
        LayoutRebuilder.ForceRebuildLayoutImmediate(infoContainer);    // This is done to ensure Layout Groups calculate properly
    }
}

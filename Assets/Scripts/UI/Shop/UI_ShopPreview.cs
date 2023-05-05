using System;
using System.Linq;
using System.Text.RegularExpressions;
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
        var itemAffixes = item.affixes;
        var affixString = itemAffixes.Aggregate("", (current, affix) =>
        {
            string humanName = Regex.Replace(affix.attribute.ToString(), "([a-z])([A-Z])", "$1 $2");
            return current + ((affix.valueMultiplier > 1 ? "+" : "") + 
                              (affix.attribute is AttributeType.Defense or AttributeType.HealthRegen ? affix.valueMultiplier + " " : Math.Round((affix.valueMultiplier * 100) - 100) + "% ") + humanName +
                              "<br>");
        });
        itemDescription.text = affixString;
        totalCost.text = "" + shopItem.cost * quantity;
        LayoutRebuilder.ForceRebuildLayoutImmediate(infoContainer);    // This is done to ensure Layout Groups calculate properly
    }
}

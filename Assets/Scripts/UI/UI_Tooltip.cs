using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Tooltip singleton manager.
/// Manages single tooltip's location and content.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
public class UI_Tooltip : MonoBehaviour
{

    [SerializeField] private Transform itemName;
    [SerializeField] private Transform itemStats;
    [SerializeField] private Transform itemDescription;
    private TextMeshProUGUI _itemNameTextMesh;
    private TextMeshProUGUI _itemStatsTextMesh;
    private TextMeshProUGUI _itemDescriptionTextMesh;
    private RectTransform _rectTransform;
    private readonly Vector3 _positionOffset = new(15, 0);

    private void Awake()
    {
        _itemNameTextMesh = itemName.GetComponent<TextMeshProUGUI>();
        _itemDescriptionTextMesh = itemDescription.GetComponent<TextMeshProUGUI>();
        _itemStatsTextMesh = itemStats.GetComponent<TextMeshProUGUI>();
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        transform.position = Input.mousePosition + _positionOffset;
    }

    public void ActiveTooltip(string text)
    {
        gameObject.SetActive(true);
        transform.position = Input.mousePosition + _positionOffset;
        _itemNameTextMesh.SetText(text);
        _itemDescriptionTextMesh.gameObject.SetActive(false);
        _itemStatsTextMesh.gameObject.SetActive(false);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);    // This is done to ensure Layout Groups calculate properly
    }

    public void ActivateItemTooltip(Item item)
    {
        gameObject.SetActive(true);
        transform.position = Input.mousePosition + _positionOffset;
        _itemNameTextMesh.SetText(item.GetName());
        _itemDescriptionTextMesh.SetText(item.GetDescription());
        _itemDescriptionTextMesh.gameObject.SetActive(item.GetDescription() != "");
        var itemAffixes = item.GetAffixes();
        var affixString = itemAffixes.Aggregate("", (current, affix) =>
        {
            string humanName = Regex.Replace(affix.attribute.ToString(), "([a-z])([A-Z])", "$1 $2");
            return current + ((affix.valueMultiplier > 1 ? "+" : "") + 
                              (affix.attribute is AttributeType.Defense or AttributeType.HealthRegen ? affix.valueMultiplier + " " : Math.Round((affix.valueMultiplier * 100) - 100) + "% ") + humanName +
                              "<br>");
        });
        _itemStatsTextMesh.SetText(affixString);
        _itemStatsTextMesh.gameObject.SetActive(itemAffixes.Count > 0);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);    // This is done to ensure Layout Groups calculate properly
    }

    public void DisableTooltip()
    {
        gameObject.SetActive(false);
    }
}

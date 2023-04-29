using System;
using System.Collections.Generic;
using System.Linq;
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

    public void ActivateItemTooltip(Item item)
    {
        gameObject.SetActive(true);
        transform.position = Input.mousePosition + _positionOffset;
        _itemNameTextMesh.SetText(item.GetName());
        _itemDescriptionTextMesh.SetText(item.GetDescription());
        var itemAffixes = item.GetAffixes();
        var affixString = itemAffixes.Aggregate("", (current, affix) => current + ("+" + Math.Round((affix.valueMultiplier * 100) - 100) + "% " + affix.attribute.HumanName() + "<br>"));
        _itemStatsTextMesh.SetText(affixString);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);    // This is done to ensure Layout Groups calculate properly
    }

    public void DisableTooltip()
    {
        gameObject.SetActive(false);
    }
}

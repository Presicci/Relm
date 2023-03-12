using TMPro;
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
    [SerializeField] private Transform itemDescription;
    private TextMeshProUGUI _itemNameTextMesh;
    private TextMeshProUGUI _itemDescriptionTextMesh;
    private RectTransform _rectTransform;
    private readonly Vector3 _positionOffset = new(15, 0);

    private void Awake()
    {
        _itemNameTextMesh = itemName.GetComponent<TextMeshProUGUI>();
        _itemDescriptionTextMesh = itemDescription.GetComponent<TextMeshProUGUI>();
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
        LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);    // This is done to ensure Layout Groups calculate properly
    }

    public void DisableTooltip()
    {
        gameObject.SetActive(false);
    }
}

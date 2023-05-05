using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ShopItem : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI cost;

    private UI_Shop _shopUI;
    private ShopItem _item;
    
    public void SetupItem(ShopItem shopItem, UI_Shop shopUI)
    {
        gameObject.SetActive(true);
        ItemScriptableObject item = shopItem.item;
        itemImage.sprite = item.sprite;
        itemName.text = item.itemName;
        cost.text = "" + shopItem.cost;
        _shopUI = shopUI;
        _item = shopItem;
    }

    public void Click()
    {
        _shopUI.ClickItem(_item);
    }
}

using UnityEngine;
using UnityEngine.UI;

public delegate void OnInventoryMoveDelegate(int originalSlot, int newSlot);

/// <summary>
/// Image singleton that is created when dragging an item from the inventory.
/// Updates to follow the cursor until mouse 0 is lifted.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
public class UI_ItemDrag : MonoBehaviour
{
    // Event for updating the UI_Inventory and player Inventory
    public static event OnInventoryMoveDelegate OnMove = delegate { };
    
    public static UI_ItemDrag Instance;

    public int toSlot;
    private UI_InventorySlot _fromSlot;
    private Image _image;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple itemdrags!");
            return;
        }

        Instance = this;
        _image = transform.GetComponent<Image>();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            gameObject.SetActive(false);
            _fromSlot.ResetAlpha();

            int fromIndex = _fromSlot.GetSlotIndex();
            if (toSlot >= 0 && fromIndex != toSlot)
                OnMove.Invoke(fromIndex, toSlot);
        }
    }

    public void DragItem(UI_InventorySlot slot)
    {
        _fromSlot = slot;
        _image.sprite = slot.GetItem().GetSprite();
        transform.position = Input.mousePosition;
        gameObject.SetActive(true);
    }
}
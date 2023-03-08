using UnityEngine;

public class PlayerPickups : MonoBehaviour
{
    private Player _player;
    
    private void Start()
    {
        _player = GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("ItemDrop"))
        {
            ItemDrop itemDrop = col.GetComponent<ItemDrop>();
            if (itemDrop == null) return;
            
            Item item = itemDrop.GetItem();
            if (item == null) return;
            
            Inventory inventory = _player.GetInventory();
            if (!inventory.HasRoomFor(1)) return;
            if (!itemDrop.Pickup()) return;
            
            inventory.AddItemToFirstAvailable(item);
        }
    }
}

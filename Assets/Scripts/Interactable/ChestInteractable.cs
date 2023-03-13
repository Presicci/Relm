using UnityEngine;

/// <summary>
/// Controller for interactable chests.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
public class ChestInteractable : Interactable
{
    [SerializeField] private ItemDrop itemDropPrefab;
    [SerializeField] private Sprite openedSprite;
    
    private PlayerInteraction _player;
    private SpriteRenderer _spriteRenderer;
    private bool _opened;
    
    void Start()
    {
        _spriteRenderer = transform.GetComponent<SpriteRenderer>();
    }

    public override void Interact()
    {
        if (!CanInteract) return;
        _spriteRenderer.sprite = openedSprite;
        CanInteract = false;
        ItemDrop itemDrop = Instantiate(itemDropPrefab, transform.position + new Vector3(0, 0.6f, 0), Quaternion.identity);
        itemDrop.Init(ItemDef.GetById(1));  // Tempt item for now
    }
}

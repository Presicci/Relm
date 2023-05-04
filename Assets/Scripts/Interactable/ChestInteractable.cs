using UnityEngine;

/// <summary>
/// Controller for interactable chests.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
[RequireComponent(typeof(LootTable))]
public class ChestInteractable : Interactable
{
    [SerializeField] private ItemDrop itemDropPrefab;
    [SerializeField] private Sprite openedSprite;
    [SerializeField] private UI_UpgradeSelect upgradeSelect;
    
    private SpriteRenderer _spriteRenderer;
    private LootTable _lootTable;

    void Start()
    {
        _spriteRenderer = transform.GetComponent<SpriteRenderer>();
        _lootTable = transform.GetComponent<LootTable>();
    }

    public override void Interact()
    {
        if (!CanInteract) return;
        _spriteRenderer.sprite = openedSprite;
        CanInteract = false;
        //upgradeSelect.GenerateChoices();
        ItemDrop itemDrop = Instantiate(itemDropPrefab, transform.position + new Vector3(0, 0.6f, 0), Quaternion.identity);
        itemDrop.Init(_lootTable.Roll());
    }
}

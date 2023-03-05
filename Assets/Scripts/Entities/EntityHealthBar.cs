using UnityEngine;

[RequireComponent(typeof(EntityDamageable))]
public class EntityHealthBar : MonoBehaviour
{
    private MaterialPropertyBlock _materialPropertyBlock;
    private SpriteRenderer _spriteRenderer;
    private EntityDamageable _entityDamageable;
    private static readonly int Fill = Shader.PropertyToID("_Fill");

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _entityDamageable = GetComponentInParent<EntityDamageable>();
        _materialPropertyBlock = new MaterialPropertyBlock();
    }

    private void Update()
    {
        var currentHealth = _entityDamageable.CurrentHealth;
        var maxHealth = _entityDamageable.maxHealth;
        if (currentHealth < maxHealth)
        {
            _spriteRenderer.enabled = true;
            _spriteRenderer.GetPropertyBlock(_materialPropertyBlock);
            _materialPropertyBlock.SetFloat(Fill, (float) currentHealth / maxHealth);
            _spriteRenderer.SetPropertyBlock(_materialPropertyBlock);
        }
        else
        {
            _spriteRenderer.enabled = false;
        }
    }
}

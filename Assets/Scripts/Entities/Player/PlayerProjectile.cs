using UnityEngine;

/// <summary>
/// Attached to any projectiles the player creates.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
public class PlayerProjectile : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Remove object when it goes off-screen
        if (!_spriteRenderer.isVisible)
        {
            Destroy(gameObject);
        }
    }
}

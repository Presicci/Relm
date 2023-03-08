using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Item drop entity.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
public class ItemDrop : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Item _item;
    
    public void Init(Item item)
    {
        _item = item;
        _spriteRenderer.sprite = _item.GetSprite();
    }
    
    private void Awake()
    {
        _rigidbody2D = transform.GetComponent<Rigidbody2D>();
        _spriteRenderer = transform.GetComponent<SpriteRenderer>();
        _rigidbody2D.AddForce(new Vector2(Random.Range(-0.6f, 0.6f), 6), ForceMode2D.Impulse);
        StartCoroutine(RemoveGravity());
    }

    private IEnumerator RemoveGravity()
    {
        yield return new WaitForSeconds(1f);
        _rigidbody2D.gravityScale = 0f;
        _rigidbody2D.velocity = Vector2.zero;
    }
}

using System;
using UnityEngine;

public class EnemyDumbAI : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private SpriteRenderer _spriteRenderer;
    public int damage;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Flip(bool flip)
    {
        _spriteRenderer.flipX = flip;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
}

using System;
using System.Collections;
using UnityEngine;

public class PlayerDamageable : Damageable
{
    [SerializeField] private GameObject gameOverScreen;
    private SpriteRenderer _spriteRenderer;
    private PlayerAttributes _playerAttributes;
    private bool _invulnerability;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerAttributes = GetComponent<PlayerAttributes>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (_invulnerability) return;
        if (!col.transform.CompareTag("AggressiveEnemy")) return;
        EnemyDumbAI enemy = col.transform.GetComponent<EnemyDumbAI>();
        if (enemy == null) return;
        Damage(enemy.damage);
    }

    protected override void Die()
    {
        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);
    }
    
    protected override int GetDefense()
    {
        Debug.Log(_playerAttributes.GetAttributeValue(AttributeType.Defense) + " " + (int) _playerAttributes.GetAttributeValue(AttributeType.Defense));
        return (int) _playerAttributes.GetAttributeValue(AttributeType.Defense);
    }

    protected override void OnDamageTaken()
    {
        StartCoroutine(InvulnerabilityFrames());
    }

    private IEnumerator InvulnerabilityFrames()
    {
        _invulnerability = true;
        float time = 0f;
        while (time < 1f)
        {
            Color color = time > 0.5f 
                ? Color.Lerp(Color.red, Color.white, Mathf.PingPong((time - 0.5f) * 2, 1)) 
                : Color.Lerp(Color.white, Color.red, Mathf.PingPong(time * 2, 1));
            _spriteRenderer.color = color;
            time += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        _spriteRenderer.color = Color.white;
        _invulnerability = false;
    }

    public void ToggleGodMode()
    {
        if (maxHealth == int.MaxValue)
        {
            maxHealth = 100;
            CurrentHealth = maxHealth;
        }
        else
        {
            maxHealth = int.MaxValue;
            CurrentHealth = maxHealth;
        }
    }

    public void SetHealth(int value)
    {
        maxHealth = value;
        CurrentHealth = value;
    }
}

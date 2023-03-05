using System.Collections;
using UnityEngine;

public class EntityDamageable : MonoBehaviour
{
    [SerializeField] public int maxHealth;
    
    public float forceMultiplier;

    public int CurrentHealth { private set; get; }
    private bool _invulnerability;
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        CurrentHealth = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("PlayerWeapon") && !col.CompareTag("PlayerProjectile")) return;
        if (_invulnerability) return;
        Debug.Log("HIT!");
        Damage(20);
        _rigidbody2D.AddForce(forceMultiplier * (transform.position - col.transform.position).normalized, ForceMode2D.Impulse);
        if (col.CompareTag("PlayerProjectile"))
            Destroy(col.gameObject);
    }
    
    private void Damage(int damage)
    {
        if ((CurrentHealth -= damage) <= 0)
        {
            Die();
        }
        StartCoroutine(InvulnerabilityFrames());
    }

    private void Die()
    {
        Destroy(gameObject);
    }
    
    private IEnumerator InvulnerabilityFrames()
    {
        _invulnerability = true;
        yield return new WaitForSeconds(0.1f);
        _invulnerability = false;
    }
}

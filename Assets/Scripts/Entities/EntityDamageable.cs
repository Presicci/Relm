using UnityEngine;

public class EntityDamageable : Damageable
{
    public float forceMultiplier;
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        bool projectile = col.CompareTag("PlayerProjectile");
        if (!col.CompareTag("PlayerWeapon") && !projectile) return;
        Debug.Log("HIT!");
        Damage(20);
        _rigidbody2D.AddForce(forceMultiplier * (transform.position - (projectile ? col.transform.position : col.transform.parent.parent.position)).normalized, ForceMode2D.Impulse);
        if (col.CompareTag("PlayerProjectile"))
            Destroy(col.gameObject);
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }

    protected override void OnDamageTaken()
    {
        
    }
}

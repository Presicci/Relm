using UnityEngine;

public class EnemyDamageable : Damageable
{
    [SerializeField] private ExperienceOrb experienceOrb;
    [SerializeField] private int experienceReward;
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
        PlayerWeaponDamage playerWeaponDamage = col.GetComponent<PlayerWeaponDamage>();
        if (playerWeaponDamage == null) return;
        Damage(playerWeaponDamage.GetDamage());
        _rigidbody2D.AddForce(forceMultiplier * (transform.position - (projectile ? col.transform.position : col.transform.parent.parent.position)).normalized, ForceMode2D.Impulse);
        if (col.CompareTag("PlayerProjectile"))
            Destroy(col.gameObject);
    }

    protected override void Die()
    {
        Destroy(gameObject);
        ExperienceOrb orb = Instantiate(experienceOrb, transform.position, Quaternion.identity);
        orb.SetExperience(experienceReward);
    }

    protected override void OnDamageTaken()
    {
        
    }
}

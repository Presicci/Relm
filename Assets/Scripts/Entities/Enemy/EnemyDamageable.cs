using System;
using UnityEngine;

public class EnemyDamageable : Damageable
{
    [SerializeField] private ItemDrop itemDropPrefab;
    [SerializeField] private ExperienceOrb experienceOrb;
    [SerializeField] private int experienceReward;
    public float forceMultiplier;
    private Rigidbody2D _rigidbody2D;
    private LootTable _lootTable;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _lootTable = transform.GetComponent<LootTable>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        bool projectile = col.CompareTag("PlayerProjectile");
        if (!col.CompareTag("PlayerWeapon") && !projectile) return;
        PlayerWeaponDamage playerWeaponDamage = col.GetComponent<PlayerWeaponDamage>();
        if (playerWeaponDamage == null) return;
        Damage(playerWeaponDamage.GetDamage());  
        //play sound
        GetComponent<AudioSource>().Play();
        _rigidbody2D.AddForce(forceMultiplier * (transform.position - (projectile ? col.transform.position : col.transform.parent.parent.position)).normalized, ForceMode2D.Impulse);
        if (col.CompareTag("PlayerProjectile"))
            Destroy(col.gameObject);
    }

    protected override void Die()
    {
        Destroy(gameObject);
        Item item = _lootTable.Roll();
        if (item != null)
        {
            ItemDrop itemDrop = Instantiate(itemDropPrefab, transform.position + new Vector3(0, 0.6f, 0), Quaternion.identity);
            itemDrop.Init(item);
        }
        ExperienceOrb orb = Instantiate(experienceOrb, transform.position, Quaternion.identity);
        orb.SetExperience(experienceReward);
    }

    protected override void OnDamageTaken()
    {
        
    }

    public void SetMaxHealth(int value)
    {
        maxHealth = value;
        CurrentHealth = value;
    }

    public void IncreaseMaxHealth(float multiplier)
    {
        maxHealth = (int) Math.Floor(multiplier * maxHealth);
        CurrentHealth = maxHealth;
    }
}

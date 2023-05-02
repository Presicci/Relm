using UnityEngine;

public abstract class Damageable : MonoBehaviour
{
    [SerializeField] public int maxHealth;
    public int CurrentHealth { protected set; get; }

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    protected virtual int GetDefense()
    {
        return 0;
    }

    protected void Damage(int damage)
    {
        int finalDamage = damage - GetDefense();
        if ((CurrentHealth -= finalDamage) <= 0)
        {
            Die();
        }
        OnDamageTaken();
    }

    protected abstract void Die();
    protected abstract void OnDamageTaken();
}

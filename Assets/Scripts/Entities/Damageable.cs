using UnityEngine;

public abstract class Damageable : MonoBehaviour
{
    [SerializeField] public int maxHealth;
    public int CurrentHealth { protected set; get; }

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    protected void Damage(int damage)
    {
        if ((CurrentHealth -= damage) <= 0)
        {
            Die();
        }
        OnDamageTaken();
    }

    protected abstract void Die();
    protected abstract void OnDamageTaken();
}

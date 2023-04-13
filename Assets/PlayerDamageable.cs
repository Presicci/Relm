using System.Collections;
using UnityEngine;

public class PlayerDamageable : Damageable
{
    private bool _invulnerability;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (_invulnerability) return;
        if (!col.transform.CompareTag("AggressiveEnemy")) return;
        Damage(20);
    }

    protected override void Die()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnDamageTaken()
    {
        StartCoroutine(InvulnerabilityFrames());
    }

    private IEnumerator InvulnerabilityFrames()
    {
        _invulnerability = true;
        yield return new WaitForSeconds(0.5f);
        _invulnerability = false;
    }
}

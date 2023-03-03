using System.Collections;
using UnityEngine;

/// <summary>
/// Enemy AI controller.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
public class EnemyAI : MonoBehaviour
{
    public float forceMultiplier;
    private Rigidbody2D _rigidbody2D;
    private bool _invulnerability;
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (_invulnerability) return;
        Debug.Log("HIT!");
        // TODO damage handling
        // TODO knock away from player instead of collider
        _rigidbody2D.AddForce(forceMultiplier * (transform.position - col.transform.position).normalized, ForceMode2D.Impulse);
        if (col.CompareTag("PlayerProjectile"))
            Destroy(col.gameObject);
        StartCoroutine(InvulnerabilityFrames());
    }
    
    private IEnumerator InvulnerabilityFrames()
    {
        _invulnerability = true;
        yield return new WaitForSeconds(0.1f);
        _invulnerability = false;
    }
}

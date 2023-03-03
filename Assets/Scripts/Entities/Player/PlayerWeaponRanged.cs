using System.Collections;
using UnityEngine;

/// <summary>
/// Handles attacking and animations relating to ranged weapons.
/// Extends PlayerWeapon.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
public class PlayerWeaponRanged : PlayerWeapon
{
    [SerializeField] private Texture2D cursor;
    [SerializeField] private Rigidbody2D projectilePrefab;
    [SerializeField] private float attspeed;
    private SpriteRenderer _spriteRenderer;
    private Vector3 _targetPos;
    private Quaternion _targetRot;

    private void Awake()
    {
        Cursor.SetCursor(cursor, new Vector2(156, 156), CursorMode.Auto);
    }

    private void OnEnable()
    {
        Cursor.SetCursor(cursor, new Vector2(156, 156), CursorMode.Auto);
    }

    private void OnDisable()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public override void Attack()
    {
        Vector3 shootDirection = Input.mousePosition;
        shootDirection.z = 0f;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection - transform.position;
        Rigidbody2D projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as Rigidbody2D;
        projectile.velocity = new Vector2(shootDirection.x * attspeed, shootDirection.y * attspeed);
        //StartCoroutine(AttackCooldown(1));
    }
    
    IEnumerator AttackCooldown(float attackTime)
    {
        
        //Quaternion rotation = transform.rotation;
        //transform.localScale = new Vector3(2f, 2f, 0);
        //_targetRot = rotation * new Quaternion(0f, 0f, 45f, 0f);
        yield return new WaitForSeconds(attackTime);
        //transform.localScale = new Vector3(1f, 1f, 0);
        //_targetPos = transform.position;
        //_targetRot = rotation;
        yield return new WaitForSeconds(0.1f);
    }
}

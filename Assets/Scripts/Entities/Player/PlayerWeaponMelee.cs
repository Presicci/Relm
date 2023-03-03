using UnityEngine;

/// <summary>
/// Handles attacking and animations relating to melee weapons.
/// Extends PlayerWeapon.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
public class PlayerWeaponMelee : PlayerWeapon
{
    [SerializeField] private GameObject weapon;
    [SerializeField] private Transform weaponIdle;
    [SerializeField] private bool mouseControl;
    [SerializeField] private float attackSpeed = 300f;
    
    private bool _attacking;
    private Vector3 _direction = Vector3.zero;
    private int _lastDirection;
    private float _animCounter;
    private Vector3 _startPosition;
    private Vector3 _startRotation;
    
    private TrailRenderer _trailRenderer;
    
    private void Awake()
    {
        _trailRenderer = weapon.GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForAttackInput();
        if (_attacking)
        {
            AttackAnimUpdate();
        }
        _direction = Vector3.zero;
        if (GameInput.KeyCheck(KeyCode.W))
        {
            _direction.y = 1;
            _lastDirection = 1;
        }
        else if (GameInput.KeyCheck(KeyCode.S))
        {
            _direction.y = -1;
            _lastDirection = 0;
        }
        if (GameInput.KeyCheck(KeyCode.A))
        {
            _direction.x = -1;
            _lastDirection = 2;
        }
        else if (GameInput.KeyCheck(KeyCode.D))
        {
            _direction.x = 1;
            _lastDirection = 3;
        }
        if (_direction == Vector3.zero)
        {
            _direction = new Vector3(
                _lastDirection == 2 ? -1 : _lastDirection == 3 ? 1 : 0,
                _lastDirection == 0 ? -1 : _lastDirection == 1 ? 1 : 0,
                1
            );
        }
    }

    private void AttackAnimUpdate()
    {
        Vector3 pos = new Vector3 ( 
            _startPosition.x + (_animCounter / 90f), 
            _startPosition.y - (Mathf.Sin(Mathf.PI * 2 * _animCounter / 360) / 1.3f),
            _startPosition.z
        );
        Vector3 rot = new Vector3(
            _startRotation.x,
            _startRotation.y,
            _startRotation.z + _animCounter
        );
        transform.localPosition = Vector3.Lerp( transform.position, pos, 1f);
        transform.localEulerAngles = Vector3.Lerp(transform.eulerAngles, rot, 1f);
        _animCounter += attackSpeed * Time.deltaTime;
        if (_animCounter >= 180)
        {
            _trailRenderer.emitting = false;
            transform.localPosition = _startPosition;
            transform.localEulerAngles = _startRotation;
            weapon.SetActive(false);
            weaponIdle.localScale = Vector3.one;
            _attacking = false;
        }
    }

    public override void Attack()
    {
        if (_attacking) return;
        if (mouseControl)
        {
            Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.parent.position;
            transform.parent.rotation = Quaternion.Euler(0, 0, (180/Mathf.PI) * Mathf.Atan2(dir.y, dir.x) + 90f);
        }
        else
        {
            Vector3 dir = _direction;
            transform.parent.rotation = Quaternion.Euler(0, 0, (180/Mathf.PI) * Mathf.Atan2(dir.y, dir.x) + 90f);
        }
        _startPosition = new Vector2(-1f, -1f);
        transform.localPosition = _startPosition;
        _startRotation = new Vector3(0, 0, 90f);
        _attacking = true;
        _animCounter = 0;

        _trailRenderer.emitting = true;
        _trailRenderer.Clear();
        
        weapon.SetActive(true);
        weaponIdle.localScale = Vector3.zero;
    }
}

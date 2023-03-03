using UnityEngine;

/// <summary>
/// Handles idle animations for the player's melee weapon.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
public class PlayerWeaponMeleeIdle : MonoBehaviour
{
    [SerializeField] private Transform forwardLeft;
    [SerializeField] private Transform forwardRight;
    [SerializeField] private Transform backLeft;
    [SerializeField] private Transform backRight;
    public float weaponFollowSpeed;
    
    private SpriteRenderer _spriteRenderer;
    private Vector3 _targetPos;
    private Quaternion _targetRot;

    private void Awake()
    { 
        _spriteRenderer = transform.GetComponent<SpriteRenderer>();
        _targetPos = transform.position;
        _targetRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameInput.KeyCheck(KeyCode.W))
        {
            _targetPos = backRight.position;
            _targetRot = backRight.rotation;
            _spriteRenderer.sortingOrder = 1;
        }
        else if (GameInput.KeyCheck(KeyCode.S))
        {
            _targetPos = forwardLeft.position;
            _targetRot = forwardLeft.rotation;
            _spriteRenderer.sortingOrder = 3;
        }
        else if (GameInput.KeyCheck(KeyCode.A))
        {
            _targetPos = backLeft.position;
            _targetRot = backLeft.rotation;
            _spriteRenderer.sortingOrder = 1;
        }
        else if (GameInput.KeyCheck(KeyCode.D))
        {
            _targetPos = forwardRight.position;
            _targetRot = forwardRight.rotation;
            _spriteRenderer.sortingOrder = 3;
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, _targetRot, weaponFollowSpeed * 5 * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, _targetPos, weaponFollowSpeed * Time.deltaTime);
    }
}

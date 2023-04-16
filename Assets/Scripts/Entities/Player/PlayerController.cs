using UnityEngine;

/// <summary>
/// Controller for the player character.
/// Mainly input handling.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject developerConsole;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private float movementSpeed;
    
    private PlayerAttributes _playerAttributes;
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerAttributes = GetComponent<PlayerAttributes>();
    }

    void Update()
    {
        HandleInputs();
    }

    private void HandleInputs()
    {
        HandleMovement();
        if (GameInput.KeyDownCheck(KeyCode.BackQuote))
        {
            developerConsole.SetActive(true);
        }

        if (GameInput.KeyDownCheck(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void HandleMovement()
    {
        Vector2 dir = Vector2.zero;
        if (GameInput.KeyCheck(KeyCode.A))
        {
            dir.x = -1;
            _animator.SetInteger("Direction", 3);
        }
        else if (GameInput.KeyCheck(KeyCode.D))
        {
            dir.x = 1;
            _animator.SetInteger("Direction", 2);
        }

        if (GameInput.KeyCheck(KeyCode.W))
        {
            dir.y = 1;
            _animator.SetInteger("Direction", 1);
        }
        else if (GameInput.KeyCheck(KeyCode.S))
        {
            dir.y = -1;
            _animator.SetInteger("Direction", 0);
        }

        dir.Normalize();
        _animator.SetBool("IsMoving", dir.magnitude > 0);

        _rigidbody2D.velocity = (movementSpeed * _playerAttributes.GetAttributeValue(AttributeType.MovementSpeed)) * dir;
    }
}
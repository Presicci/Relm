using UnityEngine;

/// <summary>
/// Controller for the player character.
/// Mainly input handling.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject upgradeSelect;
    [SerializeField] private GameObject developerConsole;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject statsMenu;
    [SerializeField] private GameObject characterPage;
    [SerializeField] private GameObject shop;
    [SerializeField] private float movementSpeed;
    [SerializeField] private UI_Tooltip tooltip;
    
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
            TogglePauseMenu();
        }
        if (GameInput.KeyDownCheck(KeyCode.B))
        {
            ToggleCharacterPage();
        }

        if (GameInput.KeyDownCheck(KeyCode.C))
        {
            statsMenu.gameObject.SetActive(!statsMenu.activeInHierarchy);
        }
    }
    
    public void TogglePauseMenu()
    {
        var active = pauseMenu.activeInHierarchy;
        pauseMenu.SetActive(!active);
        if (active)
        {
            ContinueGame();
        }
        else
        {
            Time.timeScale = 0f;
        }
    }

    public void ToggleCharacterPage()
    {
        var active = characterPage.activeInHierarchy;
        characterPage.SetActive(!active);
        if (active)
        {
            tooltip.DisableTooltip();
            ContinueGame();
        }
        else
        {
            Time.timeScale = 0f;
        }
    }

    public void CloseShop()
    {
        shop.SetActive(false);
        ContinueGame();
    }
    
    public void ContinueGame()
    {
        if (pauseMenu.activeInHierarchy) return;
        if (characterPage.activeInHierarchy) return;
        if (upgradeSelect.activeInHierarchy) return;
        if (shop.activeInHierarchy) return;
        Time.timeScale = 1f;
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
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthbar : MonoBehaviour
{
    [SerializeField] private Image healthBarFill;
    [SerializeField] private RectTransform healthBar;
    [SerializeField] private TextMeshProUGUI healthBarText;
    private PlayerDamageable _playerDamageable;

    private void Start()
    {
        _playerDamageable = transform.GetComponent<PlayerDamageable>();
    }

    private void Update()
    {
        healthBarFill.fillAmount = Mathf.Clamp((float) _playerDamageable.CurrentHealth / _playerDamageable.maxHealth, 0, 1f);
        healthBarText.text = _playerDamageable.CurrentHealth + "";
    }
}

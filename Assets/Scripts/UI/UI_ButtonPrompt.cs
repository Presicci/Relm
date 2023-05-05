using TMPro;
using UnityEngine;

/// <summary>
/// Button prompt singleton telling the player to hit F to use an interactable.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
public class UI_ButtonPrompt : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    public bool forceUpdate;
    private Vector3 _lastPlayerPos;

    public void UpdatePosition(Interactable interactable, Vector3 playerPosition)
    {
        if (!gameObject.activeInHierarchy) return;
        if (_lastPlayerPos == transform.position && !forceUpdate) return;
        transform.position = interactable.transform.position + interactable.GetPromptOffset();
        textMesh.text = interactable.GetPrompt();
        forceUpdate = false;
        _lastPlayerPos = playerPosition;
    }
}

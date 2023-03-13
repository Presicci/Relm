using UnityEngine;

/// <summary>
/// Extended by types of Interactables.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
public abstract class Interactable : MonoBehaviour
{
    protected Vector3 PromptOffset = new (0, 1.3f);    // Default offset, can be overwritten
    protected bool CanInteract = true;

    public abstract void Interact();

    public bool IsInteractable()
    {
        return CanInteract;
    }

    public Vector3 GetPromptOffset()
    {
        return PromptOffset;
    }
}
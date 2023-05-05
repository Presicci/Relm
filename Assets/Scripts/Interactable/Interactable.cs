using UnityEngine;

/// <summary>
/// Extended by types of Interactables.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
public abstract class Interactable : MonoBehaviour
{
    private readonly Vector3 _promptOffset = new (0, 1.3f);    // Default offset, can be overwritten
    private const string Prompt = "F to open";
    protected bool CanInteract = true;

    public abstract void Interact();

    public bool IsInteractable()
    {
        return CanInteract;
    }

    public virtual Vector3 GetPromptOffset()
    {
        return _promptOffset;
    }

    public virtual string GetPrompt()
    {
        return Prompt;
    }
}
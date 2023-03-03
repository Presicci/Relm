using UnityEngine;

/// <summary>
/// Extended by types of Interactables.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
public abstract class Interactable : MonoBehaviour
{
    public Vector3 promptOffset;
    protected bool interactable;
    public abstract void Interact();
}
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private UI_ButtonPrompt buttonPrompt;
    
    private List<Interactable> _currentInteractableTransforms;

    private void Awake()
    {
        _currentInteractableTransforms = new List<Interactable>();
    }

    private void Update()
    {
        if (_currentInteractableTransforms.Count <= 0) return;
        Interactable closestInteractable = _currentInteractableTransforms[0];
        float closestDistance = Single.PositiveInfinity;
        foreach (Interactable interactable in _currentInteractableTransforms)
        {
            float distance = Vector2.Distance(interactable.transform.position, transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestInteractable = interactable;
            }
        }
        if (GameInput.KeyDownCheck(KeyCode.F))
        {
            closestInteractable.Interact();
            _currentInteractableTransforms.Remove(closestInteractable);
            UpdateButtonPrompt();
        }
        buttonPrompt.UpdatePosition(closestInteractable, transform.position);
    }

    private void UpdateButtonPrompt()
    {
        if (_currentInteractableTransforms.Count <= 0)
        {
            buttonPrompt.gameObject.SetActive(false);
            return;
        }
        buttonPrompt.forceUpdate = true;
        buttonPrompt.gameObject.SetActive(true);
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Interactable"))
        {
            Interactable interactable = col.GetComponent<Interactable>();
            if (interactable == null) return;
            if (!interactable.IsInteractable()) return;
            _currentInteractableTransforms.Add(interactable);
            UpdateButtonPrompt();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Interactable"))
        {
            _currentInteractableTransforms.Remove(col.GetComponent<Interactable>());
            UpdateButtonPrompt();
        }
    }
}

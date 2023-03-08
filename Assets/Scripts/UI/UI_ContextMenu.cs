using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_ContextMenu : MonoBehaviour, IPointerExitHandler
{
    [SerializeField] private UI_ContextMenuEntry menuEntryPrefab;
    private readonly Vector3 _offset = new(0f, 5f);
    
    public void SetupMenu(Vector3 position, params (string, Action)[] entries)
    {
        gameObject.SetActive(true);
        transform.position = position + _offset;
        foreach (var entry in entries)
        {
            UI_ContextMenuEntry entryObject = Instantiate(menuEntryPrefab, transform);
            entryObject.SetupEntry(entry);
        }
    }

    public void DestroyMenu()
    {
        for (int index = 1; index < transform.childCount; index++)
        {
            Destroy(transform.GetChild(index).gameObject);
        }
        gameObject.SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DestroyMenu();
    }
}

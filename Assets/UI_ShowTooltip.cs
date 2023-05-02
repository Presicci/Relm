using UnityEngine;
using UnityEngine.EventSystems;

public class UI_ShowTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private UI_Tooltip tooltip;
    [SerializeField] private string text;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.ActiveTooltip(text);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.DisableTooltip();
    }
}

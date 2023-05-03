using TMPro;
using UnityEngine;

public class UI_CharacterStatsElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI valueTextMesh;
    [SerializeField] private AttributeType attributeType;

    public void UpdateValue(PlayerAttributes playerAttributes)
    {
        valueTextMesh.SetText(playerAttributes.GetAttributeValue(attributeType) + (attributeType is AttributeType.Defense or AttributeType.HealthRegen ? "" : "x"));
    }
}

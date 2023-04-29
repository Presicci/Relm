using UnityEngine;

/// <summary>
/// Upgrade scriptable object.
/// Used to create upgrade assets. 
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Upgrade")]
public class UpgradeScriptableObject : ScriptableObject
{
    public string upgradeDescription;
    public AttributeType upgradeAttribute;
    public AttributeClass attributeClass;
    public float increase;
}
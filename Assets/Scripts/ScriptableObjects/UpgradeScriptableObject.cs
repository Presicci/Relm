using UnityEngine;

/// <summary>
/// Upgrade scriptable object.
/// Used to create upgrade assets. 
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Upgrade")]
public class UpgradeScriptableObject : ScriptableObject
{
    public string upgradeName;
    public AttributeType upgradeAttribute;
    public float increase;
}
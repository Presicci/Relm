using System;
using UnityEngine;

public class PlayerWeaponDamage : MonoBehaviour
{
    [SerializeField] private int baseDamage;
    [SerializeField] private PlayerAttributes playerAttributes;

    private void FixedUpdate()
    {
        var scale = playerAttributes.GetAttributeValue(AttributeType.WeaponSize);
        transform.localScale = new Vector3(scale, scale, 1);
    }

    public int GetDamage()
    {
        return (int) Math.Ceiling(baseDamage * playerAttributes.GetAttributeValue(AttributeType.Damage));
    }
}

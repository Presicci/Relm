using System;
using UnityEngine;

public class PlayerWeaponDamage : MonoBehaviour
{
    [SerializeField] private int baseDamage;
    [SerializeField] private PlayerAttributes playerAttributes;

    public int GetDamage()
    {
        return (int) Math.Ceiling(baseDamage * playerAttributes.GetAttributeValue(AttributeType.Damage));
    }
}

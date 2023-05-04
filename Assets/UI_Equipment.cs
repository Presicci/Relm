using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UI_Equipment : MonoBehaviour
{
    [SerializeField] private List<UI_EquipmentSlot> slots = new List<UI_EquipmentSlot>();
    [SerializeField] private Player player;
    private Dictionary<AttributeType, float> _attributes;
        
    private void Awake()
    {
        _attributes = new Dictionary<AttributeType, float>();
        foreach (AttributeType attribute in Enum.GetValues(typeof(AttributeType)))
        {
            if (attribute is AttributeType.Defense or AttributeType.HealthRegen)
                _attributes.Add(attribute, 0);
            else 
                _attributes.Add(attribute, 1f);
        }
    }
    
    public void RefreshStats()
    {
        foreach (var key in _attributes.Keys.ToList())
        {
            _attributes[key] = 1f;
        }
        foreach (var slot in slots)
        {
            if (slot.GetItem() == null) continue;
            foreach (var affix in slot.GetItem().GetAffixes())
            {
                _attributes[affix.attribute] += (affix.valueMultiplier - 1f);
            }
        }
        foreach (var key in _attributes.Keys.ToList())
        {
            Debug.Log(key + ":" + _attributes[key]);
        }
    }

    public void EquipItem(Item item)
    {
        foreach (var slot in slots)
        {
            if (slot.equipSlot != item.GetEquipSlot()) continue;
            if (slot.GetItem() == null)
            {
                slot.EquipItem(item);
            }
            else 
            {
                player.GetInventory().AddItemToFirstAvailable(slot.GetItem());
                slot.EquipItem(item);
            }
            return;
        }
    }
    
    public Dictionary<AttributeType, float> GetAttributes()
    {
        return _attributes;
    }
}

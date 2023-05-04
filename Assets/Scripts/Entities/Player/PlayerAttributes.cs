using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
        [SerializeField] private UI_Equipment equipment;
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

        public float GetAttributeValue(AttributeType type)
        {
                Dictionary<AttributeType, float> equipmentAttributes = equipment.GetAttributes();
                if (type is AttributeType.Defense or AttributeType.HealthRegen)
                        return _attributes[type] + (equipmentAttributes == null ? 0f : equipmentAttributes[type]);
                else 
                        return _attributes[type] * (equipmentAttributes == null ? 1f : equipmentAttributes[type]);
        }

        public void IncrementAttribute(AttributeType type, float value)
        {
                _attributes[type] += value;
        }

        public void SetAttributeValue(AttributeType type, float value)
        {
                _attributes[type] = value;
        }
}


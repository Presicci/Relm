using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
        private Dictionary<AttributeType, float> _attributes;
        
        private void Awake()
        {
                _attributes = new Dictionary<AttributeType, float>();
                foreach (AttributeType attribute in Enum.GetValues(typeof(AttributeType)))
                {
                        _attributes.Add(attribute, 1f);
                }
        }

        public float GetAttributeValue(AttributeType type)
        {
                return _attributes[type];
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


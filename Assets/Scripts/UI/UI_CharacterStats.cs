using System;
using Unity.VisualScripting;
using UnityEngine;

public class UI_CharacterStats : MonoBehaviour
{
    [SerializeField] private PlayerAttributes playerAttributes;
    [SerializeField] private Transform statsContainer;
    private UI_CharacterStatsElement[] _children;

    private void Start()
    {
        _children = statsContainer.GetComponentsInChildren<UI_CharacterStatsElement>();
    }

    private void OnEnable()
    {
        _children ??= statsContainer.GetComponentsInChildren<UI_CharacterStatsElement>();
        foreach (var child in _children)
        {
            child.UpdateValue(playerAttributes);
        }
    }
}

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles control and population of suggestion popup for the dev console.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
public class UI_DevConsoleSuggestion : MonoBehaviour
{
    [SerializeField] private UI_DevConsole console;
    [SerializeField] private TextMeshProUGUI fillerText;
    [SerializeField] private TextMeshProUGUI elementPrefab;
    [SerializeField] private Transform container;

    private int _index;
    private List<TextMeshProUGUI> _suggestions;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (_index >= _suggestions.Count)
                _index = 1;
            else
                ++_index;
            for (int i = 0; i < container.childCount; i++)
            {
                _suggestions[i].color = i == _index - 1 ? Color.green : Color.yellow;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            if (_index == 0 || _index > _suggestions.Count) return;
            console.AddSuggestion(_suggestions[_index - 1].text);
            _index = 0;
        }
    }

    public void PopulateSuggestions(string command, List<string> suggestions)
    {
        if (suggestions.Count <= 0)
        {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);
        ResetSuggestions();
        fillerText.text = command;
        foreach (string sugg in suggestions)
        {
            TextMeshProUGUI element = Instantiate(elementPrefab, container);
            element.gameObject.SetActive(true);
            element.text = sugg;
            _suggestions.Add(element);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);    // This is done to ensure Layout Groups calculate properly
    }
    
    private void ResetSuggestions()
    {
        _suggestions = new List<TextMeshProUGUI>();
        _index = 0;
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
    }

    public int GetIndex()
    {
        return _index;
    }
}

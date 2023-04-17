using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Console to allow inputting commands to support development and testing.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
public class UI_DevConsole : MonoBehaviour
{
    [SerializeField] private int historyMaxLength = 10;
    [SerializeField] private TextMeshProUGUI historyTextMesh;
    [SerializeField] private UI_DevConsoleSuggestion suggestions;

    private TMP_InputField _inputField;
    private RectTransform _rectTransform;
    private int _previousCommandCursor;
    private readonly List<string> _commandHistory = new();
    private static readonly List<Command> Commands = new()
    {
        new("resizeinventory", new List<string> { "new size" }, args => GameManager.GetPlayer().GetInventory().Resize(Convert.ToInt32(args[0]))),
        new("addgold", new List<string> { "gold amount" }, args => GameManager.GetPlayer().Gold += Convert.ToInt32(args[0])),
        new("setplayer", new List<string> { "attribute type", "attribute value" }, new List<List<string>> { new(Enum.GetNames(typeof(AttributeType))) },
            args => GameManager.GetPlayer().GetComponent<PlayerAttributes>().SetAttributeValue((AttributeType) Enum.Parse(typeof(AttributeType), args[0]), (float) Convert.ToDouble(args[1]))),
        new("levelup", new List<string>(), args => GameManager.GetPlayer().GetComponent<PlayerExperience>().ForceLevelUp()),
        new("save", new List<string>(), _ => PlayerData.Save(GameManager.GetPlayer())),
        new("load", new List<string>(), _ => GameManager.GetPlayer().LoadPlayer(PlayerData.Load()))
    };

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _inputField = GetComponent<TMP_InputField>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftAlt))
        {
            LastCommand();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftAlt))
        {
            NextCommand();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            if (suggestions.gameObject.activeInHierarchy && suggestions.GetIndex() > 0) return;
            ProcessCommand();
            _previousCommandCursor = 0;
            suggestions.gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            RemoveLastChar();
            _previousCommandCursor = 0;
            EventSystem.current.SetSelectedGameObject(null);
            gameObject.SetActive(false);
            suggestions.gameObject.SetActive(false);
        }
    }

    public static void AddCommand(Command command)
    {
        Commands.Add(command);
    }

    public void CheckForSuggestions(string text)
    {
        if (text.EndsWith("\n") || text.EndsWith("\t"))
        {
            _inputField.text.Remove(text.Length - 1);
        }
        
        string[] commandSplit = text.Split(" ");
        List<string> similar = new List<string>();
        if (commandSplit.Length <= 0) return;
        if (commandSplit.Length == 1)
        {
            foreach (var command in Command.CommandIdentifiers)
            {
                if (command.StartsWith(commandSplit[0]) && !command.Equals(commandSplit[0])) similar.Add(command);
            }
        }
        else
        {
            foreach (Command command in Commands)
            {
                if (!commandSplit[0].ToLower().Equals(command.Identifier)) continue;
                if (command.Suggestions == null) break;
                int index = commandSplit.Length - 1;
                if (command.Suggestions.Count <= index - 1) break;
                List<string> commandSuggestion = command.Suggestions[index - 1];
                foreach (string suggestion in commandSuggestion)
                {
                    if (suggestion.StartsWith(commandSplit[index]) && !suggestion.Equals(commandSplit[index])) similar.Add(suggestion);
                }
            }
        }
        suggestions.PopulateSuggestions(_inputField.text, similar);
    }

    public void AddSuggestion(string suggestion)
    {
        string[] commandSplit = _inputField.text.Split(" ");
        _inputField.text += suggestion.TrimStart(commandSplit[^1].ToCharArray()) + " ";
        _inputField.ActivateInputField();
        _inputField.caretPosition = _inputField.text.Length;
        CheckForSuggestions(_inputField.text);
    }

    private void RemoveLastChar()
    {
        if (_inputField.text.Length > 0)
        {
            _inputField.text = _inputField.text.Remove(_inputField.text.Length - 1, 1);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(WaitForInputActivation());
    }

    public IEnumerator WaitForInputActivation()
    {
        yield return 0;
        _inputField.ActivateInputField();
        CheckForSuggestions("");
    }

    private void PrintHistory()
    {
        string historyString = "";
        foreach (String command in _commandHistory)
        {
            historyString += command + "<br>";
        }

        historyTextMesh.text = historyString;
        LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);    // This is done to ensure Layout Groups calculate properly
    }

    private void RecordCommand(string cmd)
    {
        if (_commandHistory.Count >= historyMaxLength)
        {
            _commandHistory.RemoveAt(0);
        }
        _commandHistory.Add(cmd);
        PrintHistory();
    }

    private void LastCommand()
    {
        if (_commandHistory.Count <= 0) return;
        if (_previousCommandCursor >= _commandHistory.Count) return;
        _inputField.text = _commandHistory[^++_previousCommandCursor];
        _inputField.caretPosition = _inputField.text.Length;
    }
    
    private void NextCommand()
    {
        if (_commandHistory.Count <= 0) return;
        if (_previousCommandCursor <= 1) return;
        _inputField.text = _commandHistory[^--_previousCommandCursor];
        _inputField.caretPosition = _inputField.text.Length;
    }
    
    private void ProcessCommand()
    {
        var command = _inputField.text;
        _inputField.SetTextWithoutNotify("");
        ParseCommand(command);
        _inputField.ActivateInputField();
    }

    private bool ParseCommand(string cmd)
    {
        string[] commandSplit = cmd.Split(" ");
        foreach (Command command in Commands)
        {
            if (!commandSplit[0].ToLower().Equals(command.Identifier)) continue;
            int commandLength = commandSplit.Length - 1 - (commandSplit[^1].Equals("") ? 1 : 0);
            if (command.Arguments.Count != commandLength)
            {
                Debug.Log("Improper syntax: '" + command.CommandSyntax() + "'");
                break;
            }
            List<string> args = new List<string>();
            for (int index = 1; index < commandSplit.Length; index++)
            {
                if (commandSplit[index].Equals("")) continue;
                args.Add(commandSplit[index]);
            }
            command.CommandAction.Invoke(args);
            RecordCommand(cmd);
            return true;
        }
        
        return false;
    }
}

public class Command
{
    public string Identifier;
    public Action<List<string>> CommandAction;
    public List<string> Arguments;
    public List<List<string>> Suggestions;
    
    public static readonly SortedSet<string> CommandIdentifiers = new();
    
    public Command(string identifier, List<string> arguments, Action<List<string>> commandAction)
    {
        Identifier = identifier;
        Arguments = arguments;
        CommandAction = commandAction;
        
        CommandIdentifiers.Add(identifier);
    }
    
    public Command(string identifier, List<string> arguments, List<List<string>> suggestions, Action<List<string>> commandAction)
    {
        Identifier = identifier;
        Arguments = arguments;
        Suggestions = suggestions;
        CommandAction = commandAction;
        
        CommandIdentifiers.Add(identifier);
    }

    public string CommandSyntax()
    {
        string command = Identifier;
        foreach (string arg in Arguments)
        {
            command += " [" + arg + "]";
        }

        return command;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Console to allow inputting commands to support development and testing.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
public class UI_DevConsole : MonoBehaviour
{
    private TMP_InputField _inputField;

    private List<Command> _commands = new List<Command>
    {
        new("item", new List<string> { "item id" }, args => GameManager.GetPlayer().GetInventory().AddItemToFirstAvailable(ItemDef.GetById(Convert.ToInt32(args[0])))),
        new("resizeinventory", new List<string> { "new size" }, args => GameManager.GetPlayer().GetInventory().Resize(Convert.ToInt32(args[0]))),
        new("addgold", new List<string> { "gold amount" }, args => GameManager.GetPlayer().Gold += Convert.ToInt32(args[0])),
        new("save", new List<string>(), _ => PlayerData.Save(GameManager.GetPlayer())),
        new("load", new List<string>(), _ => GameManager.GetPlayer().LoadPlayer(PlayerData.Load()))
    };
    
    private void Awake()
    {
        _inputField = GetComponent<TMP_InputField>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ProcessCommand();
        }
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            RemoveLastChar();
            EventSystem.current.SetSelectedGameObject(null);
            gameObject.SetActive(false);
        }
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
        foreach (Command command in _commands)
        {
            if (!commandSplit[0].ToLower().Equals(command.Identifier)) continue;
            if (command.Arguments.Count != commandSplit.Length - 1)
            {
                Debug.Log("Improper syntax: '" + command.CommandSyntax() + "'");
                break;
            }
            List<string> args = new List<string>();
            for (int index = 1; index < commandSplit.Length; index++)
            {
                args.Add(commandSplit[index]);
            }
            command.CommandAction.Invoke(args);
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
    
    public Command(string identifier, List<string> arguments, Action<List<string>> commandAction)
    {
        Identifier = identifier;
        Arguments = arguments;
        CommandAction = commandAction;
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

using System;
using System.Collections;
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

    private void Awake()
    {
        _inputField = GetComponent<TMP_InputField>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ProcessCommand();
            _inputField.DeactivateInputField();
        }
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            EventSystem.current.SetSelectedGameObject(null);
            gameObject.SetActive(false);
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

    private bool ParseCommand(string command)
    {
        string[] commandSplit = command.Split(" ");
        switch (commandSplit[0])
        {
            case "item":
                if (commandSplit.Length > 1)
                {
                    GameManager.GetPlayer().GetInventory().AddItemToFirstAvailable(ItemDef.GetById(Convert.ToInt32(commandSplit[1])));
                    return true;
                }
                return false;
            case "resizeinventory":
                if (commandSplit.Length > 1)
                {
                    GameManager.GetPlayer().GetInventory().Resize(Convert.ToInt32(commandSplit[1]));
                    return true;
                }
                return false;
        }
        return false;
    }
}

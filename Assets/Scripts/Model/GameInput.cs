using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Process input checks to make sure
/// inputs are only accepted when we want.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
public class GameInput
{
    private static bool InputCheck()
    {
        if (EventSystem.current.currentSelectedGameObject != null) return false;    // Ensure UI is not being interracted with
        return true;
    }
    
    public static bool KeyDownCheck(KeyCode keyCode)
    {
        if (!InputCheck()) return false;
        return Input.GetKeyDown(keyCode);
    }
    
    public static bool KeyCheck(KeyCode keyCode)
    {
        if (!InputCheck()) return false;
        return Input.GetKey(keyCode);
    }
}
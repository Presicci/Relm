using UnityEngine;

/// <summary>
/// Extended by player weapon types.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
public abstract class PlayerWeapon : MonoBehaviour
{

    protected void CheckForAttackInput()
    {
        if (GameInput.KeyCheck(KeyCode.Space))
        {
            Attack();
        }
    }

    public abstract void Attack();
}

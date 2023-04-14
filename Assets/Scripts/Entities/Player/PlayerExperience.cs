using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    private int _experience;
    private int _level;

    public void AddExperience(int experience)
    {
        _experience += experience;
    }
}

using UnityEngine;

/// <summary>
/// Singleton for managing global data storage and game events.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player player;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("Multiple GameManagers!");
            Destroy(gameObject);
        } else {
            Instance = this;
        }
        
        // Loads all ItemScriptableObjects into a dictionary for referencing
        ItemDef.LoadItems();
        UpgradeDef.LoadUpgrades();

        SetTimescale(1f);
    }

    public static Player GetPlayer()
    {
        return Instance.player;
    }

    public void SetTimescale(float value)
    {
        Time.timeScale = value;
    }
}

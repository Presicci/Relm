using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public string firstLevel;

    [SerializeField] private GameObject mainButtons;
    public GameObject settingsScreen;

    public void StartGame() {
        SceneManager.LoadScene(firstLevel);
    }

    public void OpenSettings()
    {
        settingsScreen.SetActive(true);
        mainButtons.SetActive(false);
    }

    public void CloseSettings()
    {
        settingsScreen.SetActive(false);
        mainButtons.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public string firstLevel;

    public GameObject settingsScreen;

    void Start()
    {

    }

    void Update()
    {

    }

    public void StartGame() {
        SceneManager.LoadScene(firstLevel);
    }

    public void OpenSettings()
    {
        settingsScreen.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsScreen.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }
}
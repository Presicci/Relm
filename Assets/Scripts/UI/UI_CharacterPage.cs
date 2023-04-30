using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CharacterPage : MonoBehaviour
{
    public void TogglePage()
    {
        if (gameObject.activeInHierarchy)
            ClosePage();
        else
            OpenPage();
    }
    
    private void OpenPage()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    private void ClosePage()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_LoadScene : MonoBehaviour
{
    public static void LoadScene(string sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}

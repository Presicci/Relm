using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private int targetScene;

    public void LoadScene() {
        SceneManager.LoadScene(targetScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private SceneAsset targetScene;

    public void LoadScene() {
        SceneManager.LoadScene(targetScene.name);
    }
}
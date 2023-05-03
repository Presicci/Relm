using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private int targetScene;

    public string firstlevel;

    void Start()
    {

    }

    void Update()
    {

    }

    public void LoadScene() {
        SceneManager.LoadScene(targetScene);
    }
}
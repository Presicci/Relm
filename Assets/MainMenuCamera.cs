using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    [SerializeField] private float cameraSpeed;
    [SerializeField] private List<Transform> cameraPositions;
    private int _count;

    private void Start()
    {
        Time.timeScale = 1f;
        transform.position = cameraPositions[_count].position;
        _count++;
    }

    private void MoveCamera()
    {
        _count++;
        if (_count >= cameraPositions.Count)
            _count = 0;
        transform.position = cameraPositions[_count].position;
        _count++;
    }


    void Update()
    {
        var target = cameraPositions[_count].position;
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * cameraSpeed);
        if (transform.position == target)
        {
            MoveCamera();
        }
    }
}

using System;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    private float _time;

    private void Update()
    {
        _time += Time.deltaTime;
        var displayTimer = Math.Truncate(_time);
        var seconds = Math.Truncate(displayTimer % 60);
        timeText.text = (int) (displayTimer/60) + ":" + (seconds == 0 ? "00" : seconds < 10 ? "0" + seconds : seconds);
    }

    public float GetTime()
    {
        return _time;
    }
}

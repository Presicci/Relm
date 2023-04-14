using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_RainbowImageCycle : MonoBehaviour
{
    [SerializeField] private float minValue = 0.3f;
    [SerializeField] private float maxValue = 0.7f;
    [SerializeField] private float speed = 0.2f;
    [SerializeField] private bool clockwise = true;
    
    private Image _image;
    private int _currentChannel;
    private bool _increase = true;
    
    private void Awake()
    {
        _image = GetComponent<Image>();
    }
    
    void Update()
    {
        switch (_currentChannel)
        {
            case 0:
                if (_image.color.r >= maxValue || _image.color.r <= minValue)
                {
                    _currentChannel += clockwise ? 1 : 2;
                    _increase = !_increase;
                }
                break;
            case 1:
                if (_image.color.g >= maxValue || _image.color.g <= minValue)
                {
                    _currentChannel += clockwise ? 1 : -1;
                    _increase = !_increase;
                }
                break;
            case 2:
                if (_image.color.b >= maxValue || _image.color.b <= minValue)
                {
                    _currentChannel += clockwise ? -2 : -1;
                    _increase = !_increase;
                }
                break;
        }
        Color color = new Color(
            Math.Max(Math.Min(_image.color.r + ((_currentChannel == 0 ? speed * Time.deltaTime : 0) * (_increase ? 1 : -1)), maxValue), minValue), 
            Math.Max(Math.Min(_image.color.g + ((_currentChannel == 1 ? speed * Time.deltaTime : 0) * (_increase ? 1 : -1)), maxValue), minValue), 
            Math.Max(Math.Min(_image.color.b + ((_currentChannel == 2 ? speed * Time.deltaTime : 0) * (_increase ? 1 : -1)), maxValue), minValue), 
            _image.color.a);
        Debug.Log("R" + color.r + ", G" + color.g + ", B" + color.b);
        _image.color = color;
    }
}

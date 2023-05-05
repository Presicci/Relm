using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ExperienceOrb : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private int _experienceReward;
    private PlayerExperience _target;
    private float _speed;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.pitch = Random.Range(.8f, 1.2f);
    }

    public void SetExperience(int experience)
    {
        _experienceReward = experience;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("ExperienceVacuum")) return;
        _speed = 5f;
        _target = col.transform.parent.GetComponent<PlayerExperience>();
    }

    private void Update()
    {
        if (_speed == 0) return;
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, Time.deltaTime * _speed);
        if (transform.position == _target.transform.position)
        {
            _target.AddExperience(_experienceReward);
            _audioSource.Play();
            expDing();
            //Destroy(gameObject);
        }
        else
        {
            _speed += 0.03f;
        }
    }

    //teleport exp orb behind the world and play the exp noise
    private void expDing()
    {
        //play sound
        _audioSource.Play();
        _speed = 0;
        Destroy(gameObject, 1);
        spriteRenderer.color = new Color(1f, 1f, 1f, 0f);

    }

}

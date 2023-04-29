using UnityEngine;

public class ExperienceOrb : MonoBehaviour
{
    private int _experienceReward;
    private PlayerExperience _target;
    private float _speed;

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
            Destroy(gameObject);
        }
        else
        {
            _speed += 0.03f;
        }
    }
}

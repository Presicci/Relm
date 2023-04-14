using UnityEngine;

public class ExperienceOrb : MonoBehaviour
{
    private int _experienceReward;
    private bool _activated;
    private PlayerExperience _target;

    public void SetExperience(int experience)
    {
        _experienceReward = experience;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("ExperienceVacuum")) return;
        _activated = true;
        _target = col.transform.parent.GetComponent<PlayerExperience>();
    }

    private void Update()
    {
        if (!_activated) return;
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, Time.deltaTime * 5f);
        if (transform.position == _target.transform.position)
        {
            _target.AddExperience(_experienceReward);
            Destroy(gameObject);
        }
    }
}

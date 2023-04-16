using System.Collections;
using UnityEngine;

public class ExperienceVacuum : MonoBehaviour
{
    [SerializeField] private PlayerAttributes playerAttributes;
    private PlayerExperience _playerExperience;
    public float radius;
    
    private void Start()
    {
        _playerExperience = GetComponent<PlayerExperience>();
        StartCoroutine(CheckPickup());
    }

    private IEnumerator CheckPickup()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            var hitColliders = Physics2D.OverlapCircleAll(transform.position, radius + playerAttributes.GetAttributeValue(AttributeType.PickupRange));
            foreach (var col in hitColliders)
            {
                if (!col.CompareTag("ExperienceOrb")) continue;
                ExperienceOrb orb = col.GetComponent<ExperienceOrb>();
                if (orb != null)
                    orb.Activate(_playerExperience);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
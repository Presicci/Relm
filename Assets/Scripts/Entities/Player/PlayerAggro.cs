using System;
using System.Collections;
using UnityEngine;

public class PlayerAggro : MonoBehaviour
{
    public float aggroRadius;
    
    private void Start()
    {
        StartCoroutine(CheckAggro());
    }

    private IEnumerator CheckAggro()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            var hitColliders = Physics2D.OverlapCircleAll(transform.position, aggroRadius);
            foreach (var col in hitColliders)
            {
                //if (!col.CompareTag("AggressiveEnemy")) continue;
                EnemyAI enemy = col.GetComponent<EnemyAI>();
                if (enemy != null)
                    enemy.PathTowardsPlayer();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRadius);
    }
}
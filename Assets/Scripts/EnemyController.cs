using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyDumbAI enemyPrefab;
    [SerializeField] private TilemapRenderer tilemapRenderer;
    [SerializeField] private Player player;

    private readonly List<EnemyDumbAI> _activeEnemies = new List<EnemyDumbAI>();
    private int _spawnAmount = 3;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private void Update()
    {
        if (_activeEnemies.Count <= 0) return;
        foreach (var enemy in _activeEnemies)
        {
            if (enemy.IsDestroyed())
            {
                _activeEnemies.Remove(enemy);
                return;
            }
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, player.transform.position, enemy.GetMoveSpeed() * Time.deltaTime);
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            for (int index = 0; index < _spawnAmount; index++)
            {
                Vector3 offset = Random.insideUnitCircle.normalized * 9f;
                while (!tilemapRenderer.bounds.Contains(player.transform.position + offset))
                {
                    offset = Random.insideUnitCircle.normalized * 9f;
                }
                EnemyDumbAI enemy = Instantiate(enemyPrefab, player.transform.position + offset, Quaternion.identity);
                _activeEnemies.Add(enemy);
            }
        }
    }
}

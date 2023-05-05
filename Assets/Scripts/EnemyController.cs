using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyDumbAI tier1Enemy;
    [SerializeField] private EnemyDumbAI tier1Enemy2;
    [SerializeField] private EnemyDumbAI tier2Enemy;
    [SerializeField] private EnemyDumbAI tier3Enemy;
    [SerializeField] private EnemyDumbAI tier4Enemy;
    [SerializeField] private TilemapRenderer tilemapRenderer;
    [SerializeField] private Player player;
    [SerializeField] private float spawnOffset;
    
    private GameTimer _timer;
    private readonly List<EnemyDumbAI> _activeEnemies = new List<EnemyDumbAI>();

    void Start()
    {
        _timer = GetComponent<GameTimer>();
        StartCoroutine(SpawnEnemies());
    }

    private void Update()
    {
        if (_activeEnemies.Count <= 0) return;
        List<EnemyDumbAI> removedEnemies = new List<EnemyDumbAI>();
        foreach (var enemy in _activeEnemies)
        {
            if (enemy.IsDestroyed())
            {
                removedEnemies.Add(enemy);
                continue;
            }
            enemy.Flip(!(player.transform.position.x < enemy.transform.position.x));
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, player.transform.position, 
                (enemy.GetMoveSpeed() * ((_timer.GetTime()/900) + 1f)) * Time.deltaTime);
        }
        foreach (var enemy in removedEnemies)
        {
            _activeEnemies.Remove(enemy);
        }
    }

    private IEnumerator SpawnEnemies()
    {
        List<EnemyDumbAI> possibleEnemies = new List<EnemyDumbAI> { tier1Enemy, tier1Enemy2 };
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            if (possibleEnemies.Count == 2 && _timer.GetTime() >= 60f)
            {
                possibleEnemies.Add(tier2Enemy);
            } else if (possibleEnemies.Count == 3 && _timer.GetTime() >= 180f)
            {
                possibleEnemies.Add(tier3Enemy);
            } else if (possibleEnemies.Count == 4 && _timer.GetTime() >= 300f)
            {
                possibleEnemies.Add(tier4Enemy);
            }
            int spawnAmount = (int) (_timer.GetTime() / 180) + 1;
            for (int index = 0; index < spawnAmount; index++)
            {
                Vector3 offset = Random.insideUnitCircle.normalized * spawnOffset;
                var tries = 0;
                while (!tilemapRenderer.bounds.Contains(player.transform.position + offset))
                {
                    offset = Random.insideUnitCircle.normalized * spawnOffset;
                    if (tries++ > 15) break;
                }
                EnemyDumbAI enemy = Instantiate(possibleEnemies[Random.Range(0, possibleEnemies.Count)], player.transform.position + offset, Quaternion.identity);
                enemy.GetComponent<EnemyDamageable>().IncreaseMaxHealth((_timer.GetTime()/900) + 1f);
                _activeEnemies.Add(enemy);
            }
        }
    }
}

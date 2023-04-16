using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyDumbAI tier1Enemy;
    [SerializeField] private EnemyDumbAI tier2Enemy;
    [SerializeField] private EnemyDumbAI tier3Enemy;
    [SerializeField] private EnemyDumbAI tier4Enemy;
    [SerializeField] private TilemapRenderer tilemapRenderer;
    [SerializeField] private Player player;
    
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
        foreach (var enemy in _activeEnemies)
        {
            if (enemy.IsDestroyed())
            {
                _activeEnemies.Remove(enemy);
                return;
            }
            if (player.transform.position.x < enemy.transform.position.x)
                enemy.Flip(false);
            else
                enemy.Flip(true);
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, player.transform.position, enemy.GetMoveSpeed() * Time.deltaTime);
        }
    }

    private IEnumerator SpawnEnemies()
    {
        List<EnemyDumbAI> possibleEnemies = new List<EnemyDumbAI> { tier1Enemy };
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            if (possibleEnemies.Count == 1 && _timer.GetTime() >= 60f)
            {
                possibleEnemies.Add(tier2Enemy);
            } else if (possibleEnemies.Count == 1 && _timer.GetTime() >= 180f)
            {
                possibleEnemies.Add(tier3Enemy);
            } else if (possibleEnemies.Count == 2 && _timer.GetTime() >= 300f)
            {
                possibleEnemies.Add(tier4Enemy);
            }
            int spawnAmount = (int) (_timer.GetTime() / 300) + 1;
            for (int index = 0; index < spawnAmount; index++)
            {
                Vector3 offset = Random.insideUnitCircle.normalized * 9f;
                while (!tilemapRenderer.bounds.Contains(player.transform.position + offset))
                {
                    offset = Random.insideUnitCircle.normalized * 9f;
                }
                EnemyDumbAI enemy = Instantiate(possibleEnemies[Random.Range(0, possibleEnemies.Count)], player.transform.position + offset, Quaternion.identity);
                _activeEnemies.Add(enemy);
            }
        }
    }
}

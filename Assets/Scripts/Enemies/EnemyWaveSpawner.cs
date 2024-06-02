using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyWaveSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPositions;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int totalWaves = 3;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float spawnInterval = 1f;


    private int _currentWave = 0;
    private bool isSpawning = false;
    private List<GameObject> _activeEnemies = new List<GameObject>();
    [SerializeField] private int _enemiesPerWave;

    private void Update()
    {
        if (!isSpawning && _activeEnemies.Count == 0)
        {
            if (_currentWave < totalWaves)
            {
                StartCoroutine(SpawnWave());
            }
            else
            {
                Debug.Log("All Waves Completed!");
            }
        }
    }

    private IEnumerator SpawnWave()
    {
        isSpawning = true;
        _currentWave++;
        Debug.Log($"Starting wave {_currentWave}");

        int enemiesToSpawn = _currentWave * _enemiesPerWave;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }

        isSpawning = false;
    }

    private void SpawnEnemy()
    {
        Transform spawnPoint = spawnPositions[Random.Range(0, spawnPositions.Count)];
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, quaternion.identity);
        _activeEnemies.Add(enemy);
        var enemyScript = enemy.GetComponent<Enemy>();
        enemyScript.OnEnemyDestroyed += HandleEnemyDestroyed;
        
    }

    private void HandleEnemyDestroyed(GameObject enemy)
    {
        _activeEnemies.Remove(enemy);
    }
}
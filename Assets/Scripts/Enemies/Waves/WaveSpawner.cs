using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

[RequireComponent(typeof(BoxCollider))]
public class WaveSpawner : MonoBehaviour
{
    [SerializeField]
    private List<Wave> waves;

    public int CurrentWave { get; private set; } = -1;
    private int currentWaveIndex = 0;

    [Header("Events")]
    public UnityEvent<int> onStartNewWave;

    private int enemiesAlive;
    private Coroutine waveCycle;
    private BoxCollider boxCollider;
    
    private System.Random random = new ();


    private void Awake()
    {
        SortWaves();
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        StartWave();
    }

    private void SortWaves()
    {
        waves = waves
            .OrderBy(wave => wave.From)
            .ThenBy(wave => wave.To)
            .ToList();
    }

    public void StartWave()
    {
        CurrentWave++;
        currentWaveIndex = GetCurrentWaveIndex();
        onStartNewWave?.Invoke(CurrentWave);

        if (waveCycle != null)
        {
            StopCoroutine(waveCycle);
        }

        waveCycle = StartCoroutine(WaveCycle());
    }

    private IEnumerator WaveCycle()
    {
        uint totalToSpawn = waves[currentWaveIndex].AmounToSpawn;
        uint minBatchAmount = waves[currentWaveIndex].minBatchAmount;
        uint maxBatchAmount = waves[currentWaveIndex].maxBatchAmount;

        var enemyCollection = waves[currentWaveIndex].EnemyCollection;

        var waitForSeconds = new WaitForSeconds(waves[currentWaveIndex].SpawnInterval);

        while (totalToSpawn > 0)
        {
            uint currentToSpawn = (uint)Random.Range(
                totalToSpawn < minBatchAmount
                    ? totalToSpawn
                    : minBatchAmount,
                totalToSpawn < maxBatchAmount
                    ? totalToSpawn
                    : maxBatchAmount
            );

            for (int i = 0; i < currentToSpawn; i++)
            {
                var enemyGameObject = Instantiate(enemyCollection.GetRandomEnemy());
                var enemy = enemyGameObject.GetComponent<Enemy>();
                enemyGameObject.transform.position = GetRandomStartPosition();

                enemiesAlive++;

                if (enemy == null)
                {
                    Debug.LogWarning($"The prefab used doesn't contain an enemy component! (${enemyGameObject.name})");
                }
                else
                {
                    enemy.onDie.AddListener(() => enemiesAlive--);
                }
            }

            totalToSpawn -= currentToSpawn;
            yield return waitForSeconds;
        }

        waveCycle = null;
    }

    private Vector3 GetRandomStartPosition()
    {
        Vector3 center = transform.position + boxCollider.center;
        center.x += -boxCollider.size.x + (float)random.NextDouble() * boxCollider.size.x * 2;
        center.z += -boxCollider.size.z + (float)random.NextDouble() * boxCollider.size.z * 2;
        return center;
    }

    private int GetCurrentWaveIndex()
    {
        int index = waves.Count - 1;
        for (int i = 0; i < waves.Count; i++)
        {
            if (CurrentWave >= waves[i].From && CurrentWave <= waves[i].To)
            {
                index = i;
            }
        }

        return index;
    }
}
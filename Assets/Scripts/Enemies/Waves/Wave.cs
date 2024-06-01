using System;
using Enemies.Waves;
using UnityEngine;

[CreateAssetMenu(menuName = "Last Stand/Wave Preset", fileName = "WavePreset.asset")]
public class Wave : ScriptableObject
{
    
    [field: Header("Wave Options")]
    [field: SerializeField, Tooltip("This will be added on top of the time it takes to spawn all enemies (Like a cooldown)")]
    public float WaveDuration { get; private set; } = 10f;
    public float TotalWaveDuration => SpawnInterval * (AmounToSpawn/minBatchAmount) + WaveDuration;
    
    [field: SerializeField, Tooltip("Spawn interval in seconds.")]
    public float SpawnInterval { get; private set; } = 1f;
    
    [field: SerializeField]
    public uint AmounToSpawn { get; private set; }

    [field: SerializeField]
    public uint From { get; private set; } = 1;

    [field: SerializeField]
    public uint To { get; private set; } = 2;

    [SerializeField]
    private bool batchSpawn = true;

    [field: SerializeField]
    public uint minBatchAmount { get; private set; } = 1;

    [field: SerializeField]
    public uint maxBatchAmount { get; private set; } = 2;

    [field: Header("Enemies Options")]
    [field: SerializeField]
    public EnemyCollection EnemyCollection { get; private set; }
    
#if UNITY_EDITOR
    private uint previousFrom = UInt32.MinValue;
    private uint previousTo = UInt32.MaxValue;

    private uint previousMinBatchAmount = UInt32.MinValue;
    private uint previousMaxBatchAmount = UInt32.MaxValue;

    public void OnValidate()
    {
        if (From != previousFrom && From > To)
        {
            To = From + 1;
        }

        if (To != previousTo && To < From)
        {
            From = To - 1;
        }

        if (minBatchAmount != previousMinBatchAmount && minBatchAmount > maxBatchAmount)
        {
            maxBatchAmount = minBatchAmount + 1;
        }

        if (maxBatchAmount != previousMaxBatchAmount && maxBatchAmount < minBatchAmount)
        {
            minBatchAmount = maxBatchAmount - 1;
        }

        previousFrom = From;
        previousTo = To;
        previousMinBatchAmount = minBatchAmount;
        previousMaxBatchAmount = maxBatchAmount;
    }
#endif
}
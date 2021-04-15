using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("Where to spawn the enemies around")]
    [SerializeField] Transform target;

    [Tooltip("Enemy Prefab to spawn")]
    [SerializeField] GameObject enemy;
    [SerializeField] float range;
    [SerializeField] float frequency;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 3.0f, frequency);
    }

    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        Vector3 rand = Random.insideUnitSphere * range;
        Vector3 spawnPos = target.position - rand;
        spawnPos.y = 3.0f;
        Instantiate(enemy, spawnPos, Quaternion.identity);
    }
}

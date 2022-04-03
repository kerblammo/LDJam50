using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] int spawnCount;
    [SerializeField] float spawnLinearGrowth;
    [SerializeField] float spawnExponentialGrowth;
    [SerializeField] int waveCount = 0;
    [SerializeField] int spawnLimit = 200;
    [SerializeField] List<Collider2D> spawnZones;
    public void NextWave()
    {
        waveCount++;
    }
    public List<Enemy> SpawnWave()
    {
        int enemiesToSpawn = (int)(spawnCount + (waveCount * spawnLinearGrowth) + (Mathf.Pow(spawnExponentialGrowth, waveCount)));
        if (enemiesToSpawn > spawnLimit) { enemiesToSpawn = spawnLimit; }
        List<Enemy> enemies = new List<Enemy>();
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Vector3 spawnPoint = CalculateRandomSpawnPosition();
            Enemy karen = Instantiate(enemy, spawnPoint, Quaternion.identity);
            enemies.Add(karen);
        }

        return enemies;
    }

    Vector3 CalculateRandomSpawnPosition()
    {
        int i = Random.Range(0, spawnZones.Count);
        Collider2D spawnZone = spawnZones[i];
        Bounds bounds = spawnZone.bounds;
        return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                0
            );
    }

}

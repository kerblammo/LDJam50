using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] int spawnCount;
    [SerializeField] int waveCount = 0;
    [SerializeField] List<Collider2D> spawnZones;
    public void NextWave()
    {
        waveCount++;
    }
    public List<Enemy> SpawnWave()
    {
        List<Enemy> enemies = new List<Enemy>();
        for (int i = 0; i < spawnCount; i++)
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
        Debug.Log(i);
        Collider2D spawnZone = spawnZones[i];
        Bounds bounds = spawnZone.bounds;
        return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                0
            );
    }

}

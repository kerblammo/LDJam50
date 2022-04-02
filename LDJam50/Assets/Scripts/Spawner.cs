using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] int spawnCount;
    [SerializeField] int waveCount = 0;

    public void NextWave()
    {
        waveCount++;
    }
    public List<Enemy> SpawnWave()
    {
        List<Enemy> enemies = new List<Enemy>();
        for (int i = 0; i < spawnCount; i++)
        {
            Enemy karen = Instantiate(enemy, transform.position, Quaternion.identity);
            enemies.Add(karen);
        }

        return enemies;
    }

}

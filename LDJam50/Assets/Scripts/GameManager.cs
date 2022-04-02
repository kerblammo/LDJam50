using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Spawner spawner;
    List<Enemy> enemies;
    bool isPaused = false;
    public bool IsPaused { get => isPaused; }

    public void PauseGame()
    {
        isPaused = true;
    }

    public void UnPauseGame()
    {
        isPaused = false;
    }

    void Start()
    {
        BeginNextWave();    
    }

    void Update()
    {
        
    }

    IEnumerator PollForRoundEnd()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
            {
                enemies.RemoveAt(i);
                i--;
            }
        }

        if (enemies.Count == 0)
        {
            RoundEnd();
        }

        StartCoroutine(PollForRoundEnd());
    }

    void BeginNextWave()
    {
        StopAllCoroutines();
        spawner.NextWave();
        enemies = spawner.SpawnWave();
        foreach (Enemy enemy in enemies)
        {
            enemy.AcquireTarget();
            enemy.Activate();
        }

        StartCoroutine(PollForRoundEnd());
    }

    void RoundEnd()
    {
        BeginNextWave();
    }
}

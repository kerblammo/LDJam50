using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Spawner spawner;
    List<Enemy> enemies;
    [SerializeField] GameObject shopUI;
    [SerializeField] int RoundSurvivedBonus = 100;
    [SerializeField] float SpawnEnemiesWindow = 10f;
    bool isPaused = false;
    public bool IsPaused { get => isPaused; }

    int currency = 0;
    public int Currency { get => currency; }

    public void SpendCurrency(int amount)
    {
        currency -= amount;
        if (amount < 0) { amount = 0; }
    }
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
        currency = 0;
        shopUI.SetActive(false);
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

    IEnumerator SpawnEnemiesOverTime()
    {
        float spawnInterval = SpawnEnemiesWindow / enemies.Count;
        int enemiesSpawned = 0;
        while (enemiesSpawned < enemies.Count)
        {
            enemies[enemiesSpawned].AcquireTarget();
            enemies[enemiesSpawned].Activate();
            enemiesSpawned++;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void EnemyDefeated(int cashReward)
    {
        currency += cashReward;
        int defeatedEnemies = 0;
        foreach (Enemy enemy in enemies)
        {
            if (enemy.IsDefeated) { defeatedEnemies++; }
        }

        if (defeatedEnemies == enemies.Count) { RoundEnd(); }
    }

    void BeginNextWave()
    {
        StopAllCoroutines();
        spawner.NextWave();
        enemies = spawner.SpawnWave();
        foreach (Enemy enemy in enemies)
        {
            enemy.DeActivate();
        }

        StartCoroutine(SpawnEnemiesOverTime());
    }

    void RoundEnd()
    {
        foreach(Enemy enemy in enemies)
        {
            Destroy(enemy.gameObject, 1f);
        }
        OpenShop();
    }

    public void OpenShop()
    {
        PauseGame();
        if (!shopUI.activeInHierarchy)
        {
            GetPaid();
            shopUI.SetActive(true);
        }
        
    }

    public void CloseShop()
    {
        shopUI.SetActive(false);
        UnPauseGame();
        BeginNextWave();
    }

    void GetPaid()
    {
        DefensivePoint[] points = FindObjectsOfType<DefensivePoint>();
        foreach (DefensivePoint point in points)
        {
            if (!point.IsDestroyed()) { currency += point.RescueValue; }
        }
        currency += RoundSurvivedBonus;

    }
}

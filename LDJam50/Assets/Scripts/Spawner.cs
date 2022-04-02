using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] int spawnCount;
    [SerializeField] int waveCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Enemy karen = Instantiate(enemy, transform.position, Quaternion.identity);
            karen.AcquireTarget();
            karen.Activate();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

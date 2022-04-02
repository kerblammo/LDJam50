using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        Enemy karen = Instantiate(enemy, transform.position, Quaternion.identity);
        karen.AcquireTarget();
        karen.Activate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

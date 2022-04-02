using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensivePoint : MonoBehaviour
{
    bool isDestroyed = false;

    public bool IsDestroyed() => isDestroyed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isDestroyed = true;
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            foreach (Enemy enemy in enemies)
            {
                enemy.AcquireTarget();
            }
            GetComponent<Collider2D>().enabled = false;
        }
    }
}

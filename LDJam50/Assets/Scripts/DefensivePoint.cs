using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensivePoint : MonoBehaviour
{
    bool isDestroyed = false;
    [SerializeField] int rescueValue = 25;
    [SerializeField] Sprite destroyedSprite;
    [SerializeField] AudioSource crashSound;
    public int RescueValue { get => rescueValue; }

    public bool IsDestroyed() => isDestroyed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            crashSound.Play();
            isDestroyed = true;
            GetComponent<SpriteRenderer>().sprite = destroyedSprite;
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            foreach (Enemy enemy in enemies)
            {
                enemy.AcquireTarget();
            }
            GetComponent<Collider2D>().enabled = false;
        }
    }
}

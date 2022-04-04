using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float maximumLifeSpan = 1f;
    float damage;
    Vector3 target;
    Rigidbody2D rb;
    Transform stickToTarget;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Shoot(Vector3 target, float damage)
    {
        this.target = target;
        this.damage = damage;
        rb.AddForce(target * speed);
        float rot_z = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
        StartCoroutine(CleanUpSelfCoroutine());
    }

    IEnumerator CleanUpSelfCoroutine()
    {
        yield return new WaitForSeconds(maximumLifeSpan);
        GetComponent<Collider2D>().enabled = true;
        gameObject.SetActive(false);
        stickToTarget = null;
    }

    void Update()
    {
        if (stickToTarget != null)
        {
            transform.position = stickToTarget.position;
        } 
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy.IsDefeated) { return; }
            enemy.TakeDamage(damage);
            damage = 0;
            GetComponent<Collider2D>().enabled = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0;
            stickToTarget = enemy.transform;
            StopAllCoroutines();
            StartCoroutine(CleanUpSelfCoroutine());
            
            
        }
    }


}

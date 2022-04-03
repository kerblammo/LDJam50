using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody2D rb2d;
    GameManager manager;
    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        manager = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        if (!manager.IsPaused)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");

            Vector3 step = new Vector3(horizontal, vertical, 0);
            step = step.normalized;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + step * speed, speed * Time.deltaTime);

        } 
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Die();
        }
    }

    void Die()
    {
        manager.PauseGame();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody2D rb2d;
    GameManager manager;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        manager = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        if (!manager.IsPaused)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 step = new Vector3(horizontal, vertical, 0);
            step = step.normalized;

            rb2d.AddForce(step * speed * Time.deltaTime);
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

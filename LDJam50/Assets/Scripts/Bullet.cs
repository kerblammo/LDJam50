using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float maximumLifeSpan = 1f;
    Vector3 target;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Shoot(Vector3 target)
    {
        this.target = target;
        rb.AddForce(target * speed);
        transform.right = target - transform.position;
        StartCoroutine(CleanUpSelfCoroutine());
    }

    IEnumerator CleanUpSelfCoroutine()
    {
        yield return new WaitForSeconds(maximumLifeSpan);
        Destroy(gameObject);
    }

}

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
        float rot_z = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
        StartCoroutine(CleanUpSelfCoroutine());
    }

    IEnumerator CleanUpSelfCoroutine()
    {
        yield return new WaitForSeconds(maximumLifeSpan);
        Destroy(gameObject);
    }

}

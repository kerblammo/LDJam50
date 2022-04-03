using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject preferredTarget;
    GameObject player;
    GameManager manager;
    bool activated = false;
    [SerializeField] float speed;
    bool aggroedToPlayer = false;
    [SerializeField] Collider2D playerAggro;
    [SerializeField] Collider2D playerChase;
    [SerializeField] float health;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }
    public void Activate()
    {
        activated = true;
    }
    public void AcquireTarget()
    {
        preferredTarget = null;
        player = FindObjectOfType<PlayerMovement>().gameObject;
        DefensivePoint[] defensivePoints = FindObjectsOfType<DefensivePoint>();
        float min = Mathf.Infinity;
        foreach (DefensivePoint point in defensivePoints)
        {
            if (point.IsDestroyed()) { continue; }
            float distance = Vector3.Distance(transform.position, point.transform.position);
            if (distance < min)
            {
                min = distance;
                preferredTarget = point.gameObject;
            }
        }        

        if (preferredTarget == null)
        {
            preferredTarget = player;
        }

    }

    void Update()
    {
        if (activated && !manager.IsPaused)
        {
            if (!aggroedToPlayer)
            {
                if (playerAggro.bounds.Contains(player.transform.position))
                {
                    aggroedToPlayer = true;
                }
            } else
            {
                if (!playerChase.bounds.Contains(player.transform.position))
                {
                    aggroedToPlayer = false;
                }
            }

            GameObject target;
            if (aggroedToPlayer)
            {
                target = player;
            } else
            {
                target = preferredTarget;
            }
            transform.position = Vector3.MoveTowards(transform.position, 
                                                     target.transform.position, 
                                                     speed * Time.deltaTime);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) 
        {
            Destroy(gameObject);
        }
    }
}
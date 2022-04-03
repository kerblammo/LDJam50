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
    bool isDefeated = false;
    [SerializeField] int cashReward = 10;
    [SerializeField] AudioSource hitSound;
    [SerializeField] AudioSource fleeSound;
    Animator animator;
    public bool IsDefeated { get => isDefeated; }

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
    }
    public void DeActivate()
    {
        GetComponent<Collider2D>().enabled = false;
    }
    public void Activate()
    {
        GetComponent<Collider2D>().enabled = true;
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
        if (activated && !manager.IsPaused && !isDefeated)
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

            float lookDirection;
            if (transform.position.x < target.transform.position.x)
            {
                lookDirection = 1f;
            } else
            {
                lookDirection = -1f;
            }
            Vector3 scale = transform.localScale;
            scale.x = lookDirection;
            transform.localScale = scale;
        }

        if (isDefeated)
        {
            Vector3 fleeDirection = (transform.position - player.transform.position).normalized * 100;
            transform.position = Vector3.MoveTowards(transform.position, fleeDirection, speed * Time.deltaTime * 10);
        }
    }

    public void TakeDamage(float damage)
    {
        if (!hitSound.isPlaying)
        {
            hitSound.Play();
        }
        health -= damage;
        if (health <= 0) 
        {
            Die();
        }
    }

    void Die()
    {
        isDefeated = true;
        manager.EnemyDefeated(cashReward);
        DeActivate();
        fleeSound.Play();
        animator.SetTrigger("Flee");
        float lookDirection;
        if (transform.position.x < player.transform.position.x)
        {
            lookDirection = -1f;
        } else
        {
            lookDirection = 1f;
        }
        Vector3 scale = transform.localScale;
        scale.x = lookDirection;
        transform.localScale = scale;
    }
}

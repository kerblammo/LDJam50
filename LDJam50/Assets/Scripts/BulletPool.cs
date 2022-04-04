using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{

    List<Bullet> bullets;
    [SerializeField] Bullet bullet;
    int poolSize;

    public void CreatePool(int size)
    {
        poolSize = size;
        bullets = new List<Bullet>();
        Bullet tmp;
        for (int i = 0; i < size; i++)
        {
            tmp = Instantiate(bullet, transform);
            tmp.gameObject.SetActive(false);
            tmp.AssignParent(transform);
            bullets.Add(tmp);
        }
    }

    public Bullet Instantiate(Vector3 position, Quaternion rotation)
    {
        for (int i = 0; i < poolSize; i++)
        {
            if (!bullets[i].gameObject.activeInHierarchy)
            {
                bullets[i].transform.position = position;
                bullets[i].transform.rotation = rotation;
                bullets[i].gameObject.SetActive(true);
                
                return bullets[i];
            }
        }
        return null;
    }
}

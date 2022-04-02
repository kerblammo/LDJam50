using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Texture2D cursor;
    [SerializeField] Bullet bullet;


    void Start()
    {
        int cursorSize = 32;
        Vector2 hotspot = new Vector2(cursorSize / 2, cursorSize / 2);
        Cursor.SetCursor(cursor, hotspot, CursorMode.ForceSoftware);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireProjectile();
        }


    }

    private void FireProjectile()
    {
        Bullet projectile = Instantiate(bullet, transform.position, Quaternion.identity);
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 target = mousePosition - transform.position;
        projectile.Shoot(target.normalized);
    }
}

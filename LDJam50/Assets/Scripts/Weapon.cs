using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Texture2D cursor;
    [SerializeField] Bullet bullet;
    [SerializeField] WeaponProperties properties;

    bool canShoot;

    void Start()
    {
        int cursorSize = 32;
        Vector2 hotspot = new Vector2(cursorSize / 2, cursorSize / 2);
        Cursor.SetCursor(cursor, hotspot, CursorMode.ForceSoftware);

        canShoot = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            FireWeapon();
            StartCoroutine(WeaponCooldownCoroutine());
        }


    }

    private void FireWeapon()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 origin = mousePosition - transform.position;
        origin = origin.normalized * properties.spreadFactor;
        List<Vector3> targets = CalculateSpread(origin);

        foreach (Vector3 target in targets)
        {
            Bullet projectile = Instantiate(bullet, transform.position, Quaternion.identity);
            projectile.Shoot(target.normalized);
        }
        
    }

    List<Vector3> CalculateSpread(Vector3 origin)
    {
        List<Vector3> targets = new List<Vector3>();

        int projectilesToSpawn = properties.projectileCount;
        if (properties.projectileCount % 2 == 1)
        {
            // an uneven projectile count means one projectile will go directly to the crosshairs
            targets.Add(origin);
            projectilesToSpawn--;
        }

        Vector3 spreadAngle = new Vector3(0.5f, 0.5f, 0);
        int pairCount = (properties.projectileCount - 1) / 2;
        for (int i = 0; i < projectilesToSpawn; i += 2)
        {
            Vector3 target1 = origin + spreadAngle * (i + 1);
            Vector3 target2 = origin - spreadAngle * (i + 1);
            targets.Add(target1);
            targets.Add(target2);
        }

        return targets;
    }

    IEnumerator WeaponCooldownCoroutine()
    {
        canShoot = false;
        yield return new WaitForSeconds(properties.cooldown);
        canShoot = true;
    }
}

[System.Serializable]
public class WeaponProperties
{
    [SerializeField] public float cooldown;
    [SerializeField] public int projectileCount;
    [SerializeField] public float damage;
    [SerializeField][Tooltip("The greater this value, the tighter the cone.")] 
        public float spreadFactor;
}

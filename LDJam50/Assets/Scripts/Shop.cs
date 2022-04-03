using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] UpgradeValues rateOfFire;
    [SerializeField] UpgradeValues projectiles;
    [SerializeField] UpgradeValues damage;
    int rateOfFireLevel = 0;
    int projectileLevel = 0;
    int damageLevel = 0;
    GameManager manager;
    Weapon weapon;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
        weapon = FindObjectOfType<Weapon>();
    }

    public float GetRateOfFireValue()
    {
        return rateOfFire.Levels[rateOfFireLevel].value;
    }

    public int GetRateOfFireCost()
    {
        return rateOfFire.Levels[rateOfFireLevel].cost;
    }

    public bool IsRateOfFireSoldOut() => rateOfFireLevel >= rateOfFire.Levels.Count;

    public float GetProjectilesValue()
    {
        return projectiles.Levels[projectileLevel].value;
    }

    public int GetProjectilesCost()
    {
        return projectiles.Levels[projectileLevel].cost;
    }

    public bool AreProjectilesSoldOut() => projectileLevel >= projectiles.Levels.Count;

    public float GetDamageValue()
    {
        return damage.Levels[damageLevel].value;
    }

    public int GetDamageCost()
    {
        return damage.Levels[damageLevel].cost;
    }

    public bool IsDamageSoldOut() => damageLevel >= damage.Levels.Count;

    public void PurchaseRateOfFire()
    {
        manager.SpendCurrency(GetRateOfFireCost());
        weapon.properties.cooldown = GetRateOfFireValue();
        rateOfFireLevel++;
    }

    public void PurchaseProjectiles()
    {
        manager.SpendCurrency(GetProjectilesCost());
        weapon.properties.projectileCount = (int)GetProjectilesValue();
        projectileLevel++;
    }

    public void PurchaseDamage()
    {
        manager.SpendCurrency(GetDamageCost());
        weapon.properties.damage = GetDamageValue();
        damageLevel++;
    }
}

[System.Serializable]
public class UpgradeValues
{
    [SerializeField] public List<UpgradeLevel> Levels;
}

[System.Serializable]
public class UpgradeLevel
{
    [SerializeField] public float value;
    [SerializeField] public int cost;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    Weapon weapon;
    [SerializeField] TextMeshProUGUI currentRateOfFire;
    [SerializeField] TextMeshProUGUI nextRateOfFire;
    [SerializeField] Button purchaseRateOfFire;
    [SerializeField] TextMeshProUGUI currentProjectiles;
    [SerializeField] TextMeshProUGUI nextProjectiles;
    [SerializeField] Button purchaseProjectiles;
    [SerializeField] TextMeshProUGUI currentDamage;
    [SerializeField] TextMeshProUGUI nextDamage;
    [SerializeField] Button purchaseDamage;
    [SerializeField] TextMeshProUGUI wallet;

    [SerializeField] GameObject rateOfFireSoldOut;
    [SerializeField] List<GameObject> rateOfFireElements;
    [SerializeField] GameObject projectilesSoldOut;
    [SerializeField] List<GameObject> projectileElements;
    [SerializeField] GameObject damageSoldOut;
    [SerializeField] List<GameObject> damageElements;
    [SerializeField] PASystem paSystem;
    Shop shop;
    GameManager manager;

    private void Start()
    {
        weapon = FindObjectOfType<Weapon>();
        manager = FindObjectOfType<GameManager>();
        shop = GetComponent<Shop>();
    }

    private void Awake()
    {
        RefreshValues();
    }
    private void OnEnable()
    {
        RefreshValues();
        paSystem.PlayRandomLine();
    }

    void RefreshValues()
    {
        if (weapon == null)
        {
            return;
        }
        wallet.text = $"${manager.Currency}";

        if (shop.IsRateOfFireSoldOut())
        {
            rateOfFireSoldOut.SetActive(true);
            foreach(GameObject element in rateOfFireElements)
            {
                element.SetActive(false);
            }
        } else
        {
            currentRateOfFire.text = weapon.properties.cooldown.ToString();
            nextRateOfFire.text = shop.GetRateOfFireValue().ToString();
            int rateOfFireCost = shop.GetRateOfFireCost();
            purchaseRateOfFire.GetComponentInChildren<TextMeshProUGUI>().text = $"${rateOfFireCost}";
            if (manager.Currency < rateOfFireCost)
            {
                purchaseRateOfFire.interactable = false;
            }
            else
            {
                purchaseRateOfFire.interactable = true;
            }
        }
        

        if (shop.AreProjectilesSoldOut())
        {
            projectilesSoldOut.SetActive(true);
            foreach (GameObject element in projectileElements)
            {
                element.SetActive(false);
            }
        } else
        {
            currentProjectiles.text = weapon.properties.projectileCount.ToString();
            nextProjectiles.text = shop.GetProjectilesValue().ToString();
            int projectilesCost = shop.GetProjectilesCost();
            purchaseProjectiles.GetComponentInChildren<TextMeshProUGUI>().text = $"${projectilesCost}";
            if (manager.Currency < projectilesCost)
            {
                purchaseProjectiles.interactable = false;
            }
            else
            {
                purchaseProjectiles.interactable = true;
            }
        }

        
        if (shop.IsDamageSoldOut())
        {
            damageSoldOut.SetActive(true);
            foreach (GameObject element in damageElements)
            {
                element.SetActive(false);
            }
        } else
        {
            currentDamage.text = weapon.properties.damage.ToString();
            nextDamage.text = shop.GetDamageValue().ToString();
            int damageCost = shop.GetDamageCost();
            purchaseDamage.GetComponentInChildren<TextMeshProUGUI>().text = $"${damageCost}";
            if (manager.Currency < damageCost)
            {
                purchaseDamage.interactable = false;
            }
            else
            {
                purchaseDamage.interactable = true;
            }
        }

        
    }

    public void PurchaseRateOfFire()
    {
        shop.PurchaseRateOfFire();
        RefreshValues();
    }

    public void PurchaseProjectiles()
    {
        shop.PurchaseProjectiles();
        RefreshValues();
    }

    public void PurchaseDamage()
    {
        shop.PurchaseDamage();
        RefreshValues();
    }
}

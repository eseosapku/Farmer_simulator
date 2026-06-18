using TMPro;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public int money = 5000;
    public int fertilizerInventory = 3;
    public int fertilizerCost = 500;
    public int carrotSellPrice = 1500;
    public TMP_Text moneyText;
    public TMP_Text fertilizerInventoryText;
    public EduPopup popupSystem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyFertilizer()
    {
        if (money >= fertilizerCost)
        {
            money -= fertilizerCost;
            fertilizerInventory++;
            UpdateUI();
            Debug.Log("Bought fertilizer. Money: " + money + " | Inventory: " + fertilizerInventory);
        }
        else
        {
            if (popupSystem != null)
            {
                popupSystem.Show("NOT ENOUGH MONEY", "You need " + fertilizerCost + " RWF to buy a fertilizer bag. Harvest your carrots first!");
            }
        }
    }

    public bool TryUseFertilizer()
    {
        if (fertilizerInventory > 0)
        {
            fertilizerInventory--;
            UpdateUI();
            return true;
        }
        else
        {
            if (popupSystem != null)
            {
                popupSystem.Show("NO FERTILIZER", "You're out of fertilizer bags. Visit the market to buy more.");
            }
            return false;
        }
    }

    public void HarvestCarrots(int amount)
    {
        int earnings = amount * carrotSellPrice;
        money += earnings;
        UpdateUI();
        if (popupSystem != null)
        {
            popupSystem.Show("HARVEST SUCCESS!", "You sold " + amount + " carrots for " + earnings + " RWF! Total money: " + money + " RWF.");
        }
    }

    void UpdateUI()
    {
        if (moneyText != null) moneyText.text = "Money: " + money + " RWF";
        if (fertilizerInventoryText != null) fertilizerInventoryText.text = "Fertilizer Bags: " + fertilizerInventory;
    }
}



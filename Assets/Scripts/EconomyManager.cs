using TMPro;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public int money = 5000;
    public int fertiliserInventory = 3;
    public int fertiliserCost = 500;
    public int carrotSellPrice = 1500;
    public TMP_Text moneyText;
    public TMP_Text fertiliserInventoryText;
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

    public void Buyfertiliser()
    {
        if (money >= fertiliserCost)
        {
            money -= fertiliserCost;
            fertiliserInventory++;
            UpdateUI();
            Debug.Log("Bought fertiliser. Money: " + money + " | Inventory: " + fertiliserInventory);
        }
        else
        {
            if (popupSystem != null)
            {
                popupSystem.Show("NOT ENOUGH MONEY", "You need " + fertiliserCost + " RWF to buy a fertiliser bag. Harvest your carrots first!");
            }
        }
    }

    public bool TryUsefertiliser()
    {
        if (fertiliserInventory > 0)
        {
            fertiliserInventory--;
            UpdateUI();
            return true;
        }
        else
        {
            if (popupSystem != null)
            {
                popupSystem.Show("NO fertiliser", "You're out of fertiliser bags. Visit the market to buy more.");
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
        if (fertiliserInventoryText != null) fertiliserInventoryText.text = "fertiliser Bags: " + fertiliserInventory;
    }
}



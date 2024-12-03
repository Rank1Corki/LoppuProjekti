using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public string itemName;
    public int quantity;
}

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> itemsList = new List<InventoryItem>();
    public Dictionary<string, int> items = new Dictionary<string, int>();

    private void UpdateItemsList()
    {
        itemsList.Clear();
        foreach (var item in items)
        {
            itemsList.Add(new InventoryItem { itemName = item.Key, quantity = item.Value });
        }
    }

    public void AddItem(string itemName, int quantity)
    {
        itemName = itemName.Trim().ToLower();
        if (items.ContainsKey(itemName))
        {
            items[itemName] += quantity;
        }
        else
        {
            items[itemName] = quantity;
        }

        UpdateItemsList();
        Debug.Log($"Added {quantity} x {itemName} to inventory.");
    }

    public bool HasItem(string itemName, int quantity)
    {
        itemName = itemName.Trim().ToLower();
        return items.ContainsKey(itemName) && items[itemName] >= quantity;
    }

    public void RemoveItem(string itemName, int quantity)
    {
        itemName = itemName.Trim().ToLower();
        if (HasItem(itemName, quantity))
        {
            items[itemName] -= quantity;
            if (items[itemName] <= 0)
                items.Remove(itemName);

            UpdateItemsList();
            Debug.Log($"Removed {quantity} x {itemName} from inventory.");
        }
        else
        {
            Debug.LogWarning($"Not enough {itemName} in inventory.");
        }
    }

    // Clear inventory when the object is disabled (i.e., when the game stops or the object is destroyed)
    void OnDisable()
    {
        ClearInventory();
    }

    // Clear inventory method
    private void ClearInventory()
    {
        items.Clear();
        itemsList.Clear();
        Debug.Log("Inventory cleared on application quit or object disable.");
    }

    public void ShowInventory()
    {
        foreach (var item in items)
        {
            Debug.Log($"{item.Key}: {item.Value}");
        }
    }
}

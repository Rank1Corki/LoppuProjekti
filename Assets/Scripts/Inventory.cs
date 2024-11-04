using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Dictionary<string, int> items = new Dictionary<string, int>();

    public void AddItem(string itemName, int quantity)
    {
        if (items.ContainsKey(itemName))
        {
            items[itemName] += quantity;
        }
        else
        {
            items[itemName] = quantity;
        }

        Debug.Log($"Added {quantity} x {itemName} to inventory.");
    }

    public void ShowInventory()
    {
        foreach (var item in items)
        {
            Debug.Log($"{item.Key}: {item.Value}");
        }
    }
}

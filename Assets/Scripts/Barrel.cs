using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BarrelItem
{
    public string itemName; // The name of the item
    public int maxQuantity; // The maximum quantity for this item
    public bool isCannonball; // Flag to identify if the item is a cannonball

    public BarrelItem(string name, int maxQty, bool isCannonball)
    {
        itemName = name;
        maxQuantity = maxQty;
        this.isCannonball = isCannonball;
    }
}

public class Barrel : MonoBehaviour
{
    // Define possible items in the barrel
    public List<BarrelItem> possibleItems = new List<BarrelItem>
    {
        new BarrelItem("Wood", 10, false),
        new BarrelItem("CannonBall", 1, true),
        new BarrelItem("FireBall", 1, true),
        new BarrelItem("ShrapnelBall", 1, true),
        new BarrelItem("Gold", 10, false)
    };


    public List<(string, int)> OpenBarrel()
    {
        List<(string, int)> itemsInBarrel = new List<(string, int)>();

        // Filter cannonball and non-cannonball items
        List<BarrelItem> cannonballItems = possibleItems.FindAll(item => item.isCannonball);
        List<BarrelItem> nonCannonballItems = possibleItems.FindAll(item => !item.isCannonball);

        // Select one random cannonball if available
        if (cannonballItems.Count > 0)
        {
            BarrelItem randomCannonball = cannonballItems[Random.Range(0, cannonballItems.Count)];
            int quantity = Random.Range(1, randomCannonball.maxQuantity + 1);
            itemsInBarrel.Add((randomCannonball.itemName, quantity));
        }

        // Select one random non-cannonball item if available
        if (nonCannonballItems.Count > 0)
        {
            BarrelItem randomNonCannonball = nonCannonballItems[Random.Range(0, nonCannonballItems.Count)];
            int quantity = Random.Range(1, randomNonCannonball.maxQuantity + 1);
            itemsInBarrel.Add((randomNonCannonball.itemName, quantity));
        }

        return itemsInBarrel; // Return list of items found
    }
}

using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Inventory inventory; // Assign this in the Inspector

    public TurnManager turnManager;
    

    private void Start()
    {

    }

    void Update()
    {
       
    
        
        if (Input.GetMouseButtonDown(0)) // Left click and check is your turn
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                Barrel barrel = hit.collider.GetComponent<Barrel>();
                if (barrel != null && turnManager.isMyTurn(this.tag))
                {
                    List<(string, int)> items = barrel.OpenBarrel();
                    foreach (var item in items)
                    {
                        AddToInventory(item.Item1, item.Item2);
                    }
                }
                else
                {
                    Debug.LogWarning("No Barrel found!");
                }
            }
        }
    }

    public void AddToInventory(string itemName, int quantity)
    {
        if (inventory == null)
        {
            Debug.LogError("Inventory reference is null!");
            return;
        }

        // Ensure itemName is not null or empty
        if (string.IsNullOrEmpty(itemName))
        {
            Debug.LogError("Item name is null or empty!");
            return;
        }

        inventory.AddItem(itemName, quantity);
    }
}

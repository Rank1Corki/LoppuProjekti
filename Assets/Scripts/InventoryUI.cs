using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    // Player 1's UI elements for inventory quantities
    public TMP_Text woodQuantityTextPlayer1;
    public TMP_Text cannonballQuantityTextPlayer1;
    public TMP_Text fireballQuantityTextPlayer1;
    public TMP_Text shrapnelballQuantityTextPlayer1;

    // Player 2's UI elements for inventory quantities
    public TMP_Text woodQuantityTextPlayer2;
    public TMP_Text cannonballQuantityTextPlayer2;
    public TMP_Text fireballQuantityTextPlayer2;
    public TMP_Text shrapnelballQuantityTextPlayer2;

    public Inventory player1Inventory;  // Reference to Player 1's inventory
    public Inventory player2Inventory;  // Reference to Player 2's inventory

    // Update function to check for inventory changes and update UI text
    void Update()
    {
        UpdateInventoryUI(player1Inventory, woodQuantityTextPlayer1, cannonballQuantityTextPlayer1, fireballQuantityTextPlayer1, shrapnelballQuantityTextPlayer1);
        UpdateInventoryUI(player2Inventory, woodQuantityTextPlayer2, cannonballQuantityTextPlayer2, fireballQuantityTextPlayer2, shrapnelballQuantityTextPlayer2);
    }

    // Function to update UI text based on the inventory's contents
    private void UpdateInventoryUI(Inventory inventory, TMP_Text woodText, TMP_Text cannonballText, TMP_Text fireballText, TMP_Text shrapnelballText)
    {
        // Update Wood quantity
        if (inventory.HasItem("wood", 0))
        {
            woodText.text = "Wood: " + inventory.items["wood"].ToString();
        }
        else
        {
            woodText.text = "Wood: 0";
        }

        // Update Cannonball quantity
        if (inventory.HasItem("cannonball", 0))
        {
            cannonballText.text = "Cannonball: " + inventory.items["cannonball"].ToString();
        }
        else
        {
            cannonballText.text = "Cannonball: 0";
        }

        // Update Fireball quantity
        if (inventory.HasItem("fireball", 0))
        {
            fireballText.text = "Fireball: " + inventory.items["fireball"].ToString();
        }
        else
        {
            fireballText.text = "Fireball: 0";
        }

        // Update Shrapnelball quantity
        if (inventory.HasItem("shrapnelball", 0))
        {
            shrapnelballText.text = "Shrapnelball: " + inventory.items["shrapnelball"].ToString();
        }
        else
        {
            shrapnelballText.text = "Shrapnelball: 0";
        }
    }
}

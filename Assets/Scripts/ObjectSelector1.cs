using UnityEngine;

public class ObjectSelector1 : MonoBehaviour
{
    public SpriteRenderer barrelRenderer; // Sprite renderer for the cannon barrel
    public SpriteRenderer bottomRenderer; // Sprite renderer for the cannon base
    public GameObject cannonballSelectionUI; // UI for selecting cannonball type

    private bool isSelected = false; // Flag to check if the cannon is selected
    private bool ballSelection = false; // Flag to check if a cannonball type is selected
    private CannonController cannonController; // Reference to the CannonController

    private void Start()
    {
        // Validate the assigned SpriteRenderers
        if (barrelRenderer == null || bottomRenderer == null)
        {
            Debug.LogError("Barrel or Bottom SpriteRenderer is not assigned in the inspector.");
        }

        // Initially disable the cannonball selection UI
        if (cannonballSelectionUI != null)
        {
            cannonballSelectionUI.SetActive(false);
        }

        // Get the CannonController reference
        cannonController = GetComponent<CannonController>();
    }

    private void OnMouseOver()
    {
        if (!isSelected)
        {
            SetColor(Color.yellow); // Highlight the cannon when hovered over
        }

        // Check for left mouse button click to select the cannon
        if (Input.GetMouseButtonDown(0) && !ballSelection)
        {
            SelectObject();
        }
    }

    private void OnMouseExit()
    {
        if (!isSelected)
        {
            SetColor(Color.white); // Reset color when mouse exits
        }
    }

    private void SelectObject()
    {
        if (!isSelected)
        {
            isSelected = false;

            SetColor(Color.green); // Change color to indicate selection
            if (cannonballSelectionUI != null)
            {
                cannonballSelectionUI.SetActive(true); // Show selection UI
            }
        }
    }

    public void SelectCannonball(GameObject cannonballPrefab)
    {
        // Assuming cannonballPrefabs is set in the CannonController script
        int type = System.Array.IndexOf(cannonController.cannonballPrefabs, cannonballPrefab); // Get the index of the cannonball prefab
        cannonController.SetCannonballType(type); // Set the selected cannonball type
        ballSelection = true; // Mark that a cannonball has been selected

        // Deselect the cannonball selection UI
        Deselect();
        isSelected = true;
    }

    public bool IsSelected()
    {
        return isSelected; // Return if the cannon is selected
    }

    public void Deselect()
    {
        isSelected = false; // Deselect the cannon
        SetColor(Color.white); // Reset color
        ballSelection = false; // Reset ball selection flag
        if (cannonballSelectionUI != null)
        {
            cannonballSelectionUI.SetActive(false); // Hide selection UI
        }
    }

    private void SetColor(Color color)
    {
        if (barrelRenderer != null)
        {
            barrelRenderer.color = color; // Set the color of the barrel
        }

        if (bottomRenderer != null)
        {
            bottomRenderer.color = color; // Set the color of the bottom
        }
    }
}

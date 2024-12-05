using UnityEngine;

public class CannonballSelector : MonoBehaviour
{
    public GameObject cannonballPrefab; // Assign the cannonball prefab in the inspector

    public ObjectSelector1 objectSelector;



    private void OnMouseDown()
    {
        // Check if the object was clicked
       // ObjectSelector1 objectSelector = FindObjectOfType<ObjectSelector1>(); // Find the ObjectSelector1 instance
        if (objectSelector != null)
        {
            objectSelector.SelectCannonball(cannonballPrefab); // Select this cannonball prefab
            Debug.Log("Cannonball Selected: " + cannonballPrefab.name);
        }
    }
}

using UnityEngine;

public class Cannonball : MonoBehaviour
{
    private Collider2D cannonballCollider;

    void Start()
    {
        cannonballCollider = GetComponent<Collider2D>();
        // Destroy the cannonball after 5 seconds automatically
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        // Check if the object colliding is a shrapnel piece and ignore collision
        if (collision.gameObject.CompareTag("Shrapnel"))
        {
            return; // Don't destroy the shrapnel, just exit the method
        }

        // Destroy the cannonball on collision with any other object

        Destroy(gameObject);

        ModuleInfo partInfo = collision.gameObject.GetComponent<ModuleInfo>();

        partInfo.hP -= 10;
    }

    // Call this method when spawning shrapnel to ignore collisions with the cannonball
    public void IgnoreCannonballCollision(Collider2D shrapnelCollider)
    {
        if (shrapnelCollider != null && cannonballCollider != null)
        {
            Physics2D.IgnoreCollision(shrapnelCollider, cannonballCollider);
        }
    }
}

using UnityEngine;

public class ShrapnelCannonball1 : MonoBehaviour
{
    public GameObject shrapnelPrefab; // Assign the shrapnel prefab here
    public int shrapnelCount = 4; // Number of shrapnel pieces
    public float shrapnelForce = 10f; // Force applied to each shrapnel piece
    public float explodeDelay = 1.0f; // Delay before the cannonball explodes (optional)
    public float randomSpreadAmount = 0.1f; // Small spread added to direction of shrapnel

    private bool hasExploded = false;
    private Rigidbody2D cannonballRb; // Rigidbody2D of the cannonball

    void Start()
    {
        // Get the Rigidbody2D of the cannonball to preserve velocity
        cannonballRb = GetComponent<Rigidbody2D>();

        // Optional: explode after a delay
        Invoke(nameof(Explode), explodeDelay);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasExploded)
        {
            Explode();
        }
    }

    void Explode()
    {
        hasExploded = true;

        // Ensure the cannonball has a Rigidbody2D (for velocity)
        if (cannonballRb == null)
        {
            Debug.LogError("Cannonball Rigidbody2D is missing!");
            return;
        }

        // Store the initial velocity of the cannonball
        Vector2 cannonballVelocity = cannonballRb.velocity;

        // Spawn shrapnel pieces in the direction of the cannonball's movement
        for (int i = 0; i < shrapnelCount; i++)
        {
            // Instantiate the shrapnel piece at the cannonball's position
            GameObject shrapnel = Instantiate(shrapnelPrefab, transform.position, Quaternion.identity);

            // Get the Rigidbody2D component of the shrapnel
            Rigidbody2D rb = shrapnel.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // Apply the velocity of the cannonball with a small random spread
                Vector2 randomDirection = cannonballVelocity.normalized + new Vector2(
                    Random.Range(-randomSpreadAmount, randomSpreadAmount),
                    Random.Range(-randomSpreadAmount, randomSpreadAmount)
                );

                // Normalize the final direction to ensure consistent force
                randomDirection.Normalize();

                // Apply the force to the shrapnel piece to make it move
                rb.velocity = randomDirection * shrapnelForce;
            }
        }

        // Destroy the cannonball after explosion
        Destroy(gameObject);
    }
}

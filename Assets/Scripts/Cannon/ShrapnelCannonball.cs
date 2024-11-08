using UnityEngine;

public class ShrapnelCannonball1  : MonoBehaviour
{
    public GameObject shrapnelPrefab; // Assign the shrapnel prefab here
    public int shrapnelCount = 4; // Number of shrapnel pieces
    public float shrapnelSpread = 30f; // Spread angle for shrapnel pieces
    public float shrapnelForce = 10f; // Force applied to each shrapnel piece
    public float explodeDelay = 1.0f; // Delay before the cannonball explodes (optional)

    private bool hasExploded = false;

    void Start()
    {
        // Optional: explode after a delay
        Invoke(nameof(Explode), explodeDelay);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!hasExploded)
        {
            Explode();
        }
    }

    void Explode()
    {
        hasExploded = true;

        // Spawn shrapnel pieces in a spread
        for (int i = 0; i < shrapnelCount; i++)
        {
            // Instantiate the shrapnel piece
            GameObject shrapnel = Instantiate(shrapnelPrefab, transform.position, Quaternion.identity);

            // Calculate a random spread direction
            Vector3 randomDirection = Quaternion.Euler(
                Random.Range(-shrapnelSpread, shrapnelSpread),
                Random.Range(-shrapnelSpread, shrapnelSpread),
                Random.Range(-shrapnelSpread, shrapnelSpread)
            ) * transform.forward;

            // Apply force to the shrapnel piece
            Rigidbody rb = shrapnel.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(randomDirection * shrapnelForce, ForceMode.Impulse);
            }
        }

        // Destroy the main cannonball
        Destroy(gameObject);
    }
}

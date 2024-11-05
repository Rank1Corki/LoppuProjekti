using UnityEngine;

public class Cannonball : MonoBehaviour
{
    void Start()
    {
        // Destroy the cannonball after 5 seconds automatically
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy the cannonball on collision with any object
        Destroy(gameObject);
    }
}

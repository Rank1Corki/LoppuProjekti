using UnityEngine;

public class Cannonball : MonoBehaviour
{
    private Collider2D cannonballCollider;

    private Animator animator;

    private Rigidbody2D rb;

    void Start()
    {
        cannonballCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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

        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        
        animator.Play("Explosion");

        // Destroy the cannonball on collision with any other object


        ModuleInfo partInfo = collision.gameObject.GetComponent<ModuleInfo>();

        partInfo.hP -= 10;
    }

    private void DestroyBall()
    {
        Destroy(gameObject);
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

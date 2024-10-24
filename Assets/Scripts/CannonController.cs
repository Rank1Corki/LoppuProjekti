using UnityEngine;

public class CannonController : MonoBehaviour
{
    public Transform barrel;              // Reference to the barrel transform
    public Transform shootPoint;          // Transform at the tip of the barrel where projectiles will spawn
    public LineRenderer lineRenderer;
    public float projectileSpeed = 10f;   // Speed of the cannonball (used for trajectory prediction)
    public float maxAngle = 60f;          // Maximum upward angle for the cannon
    public int trajectorySteps = 30;      // Number of steps in the line prediction
    public GameObject cannonballPrefab;   // Prefab of the cannonball
    public float gravityScale = 1f;       // Scale factor for gravity

    private void Update()
    {
        RotateBarrelTowardsMouse();

        // Draw the line only when the shoot button is held down
        if (Input.GetMouseButton(0))
        {
            DrawTrajectory();
            lineRenderer.enabled = true;
        }
        else
        {
            lineRenderer.enabled = false;
        }

        // Shoot when the shoot button is released
        if (Input.GetMouseButtonUp(0))
        {
            ShootCannonball();
        }
    }

    private void RotateBarrelTowardsMouse()
    {
        // Get mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Ensure the Z position is zero for 2D

        // Calculate the direction from the barrel to the mouse position
        Vector2 direction = (mousePosition - barrel.position).normalized;

        // Calculate the angle between the barrel's right vector and the direction to the mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Clamp the angle to ensure it only moves between 0 and the max angle (45 degrees)
        angle = Mathf.Clamp(angle, 0, maxAngle);

        // Create a target rotation based on the clamped angle
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Smoothly rotate the barrel towards the target rotation
        barrel.rotation = Quaternion.Slerp(barrel.rotation, targetRotation, Time.deltaTime * 5f); // Adjust the smoothing factor (5f) as needed
    }


    private void DrawTrajectory()
    {
        // Get the starting position and direction
        Vector2 startPoint = shootPoint.position;
        Vector2 initialVelocity = shootPoint.right * projectileSpeed;

        lineRenderer.positionCount = trajectorySteps;
        for (int i = 0; i < trajectorySteps; i++)
        {
            // Time increment for each step based on the number of steps and line length
            float time = i * 0.1f; // Adjust time step to control line smoothness

            // Calculate the position at each step using physics equations (accounting for gravity)
            Vector2 position = startPoint + initialVelocity * time + 0.5f * Physics2D.gravity * gravityScale * time * time;
            lineRenderer.SetPosition(i, position);
        }

        // Optionally: Adjust LineRenderer width for a smaller line
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
    }

    private void ShootCannonball()
    {
        // Instantiate a cannonball at the shootPoint and set its direction
        GameObject cannonball = Instantiate(cannonballPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody2D rb = cannonball.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = shootPoint.right * projectileSpeed;

            // Apply the gravity scale to the cannonball's Rigidbody2D component
            rb.gravityScale = gravityScale;
        }
    }
}
